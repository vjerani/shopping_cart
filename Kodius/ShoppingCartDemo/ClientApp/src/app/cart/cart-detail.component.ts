import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';

import { ShoppingItem } from '../shared/shopping-item';
import { PromotionItem } from '../shared/promotion-item';
import { AppState } from '../store/app-state';
import { DeleteItemAction, DeleteAllItemAction, UpdateItemAction } from '../store/shopping.actions';
import { OrderService } from '../shared/order.service';
import { Order } from '../shared/order';
import { PromotionService, PromotionCodeOutput } from '../shared/promotion.service';
import { ApplyPromotionAction, DeleteAllPromotionItemsAction } from '../store/promotion.actions';

export enum PromotionValidation {
    OK = 0,
    InvalidCode = 1,
    AllreadyAdded = 2,
    InvalidInConjuctionWithOtherCodes = 3
}

@Component({
    selector: 'app-cart-detail',
    templateUrl: './cart-detail.component.html'
})
export class CartDetailComponent implements OnInit {
    shoppingItems$: Observable<ShoppingItem[]>;
    promotionItems$: Observable<PromotionItem[]>;
    cartSubtotal: number;
    cartDiscountTotal: number;
    cartOffPriceTotal: number;
    items: ShoppingItem[];
    promotions: PromotionItem[];
    isOrderCreated = false;
    order = { products: [], promotionCodes: [] } as Order;
    quantities: any[] = [1, 2, 3];
    promotionIsValidated = false;
    promotionisValid = false;
    promotionCode: string = undefined;
    promotionValidationResult: PromotionValidation = PromotionValidation.OK;

    constructor(private store: Store<AppState>,
                public router: Router,
                private orderService: OrderService,
                private promotionService: PromotionService,
                ) { }

    ngOnInit() {
        this.shoppingItems$ = this.store.select(store => store.shopping);
        this.promotionItems$ = this.store.select(store => store.promotions);
        this.calculateSubtotal();
    }

    removeItemFromCart(id: any): void {
        this.store.dispatch(new DeleteItemAction(id));
        this.calculateSubtotal();
    }

    calculateSubtotal(): void {
        this.shoppingItems$.subscribe(x => this.items = x);
        this.promotionItems$.subscribe(x => this.promotions = x);
        this.cartSubtotal = this.items.reduce((accumulator, currentValue) => accumulator + currentValue.price, 0);
        this.cartDiscountTotal = this.promotions.reduce((accumulator, currentValue) => accumulator + currentValue.discount, 0);
        this.cartOffPriceTotal = this.promotions.reduce((accumulator, currentValue) => accumulator + currentValue.offprice, 0);
        this.cartSubtotal = (this.cartSubtotal - (this.cartSubtotal * this.cartDiscountTotal));
        this.cartSubtotal = this.cartSubtotal - this.cartOffPriceTotal;
        console.log('offprice total: ' + this.cartOffPriceTotal);
        console.log('discount total: ' + this.cartDiscountTotal);
    }

    onQuantityChange(shoppingItem: ShoppingItem) {
        const quantity: number = Number(shoppingItem.quantity);
        shoppingItem.quantity = quantity;
        shoppingItem.price = shoppingItem.product.productPrice * Number(quantity);
        this.store.dispatch(new UpdateItemAction(shoppingItem));
        this.calculateSubtotal();
    }

    proceedToCheckout(): void {
        const newOrder = new Order();
        newOrder.products = this.items;
        newOrder.orderTotal = this.cartSubtotal;
        newOrder.promotionCodes = this.promotions.map(x =>  x.label);

        this.orderService.add(newOrder)
            .subscribe(
                (response: any) => this.order = response,
                (error: any) => { },
                () => {
                    this.afterOrderCreated();
                }
            );
    }

    addPromotion(): void {
        this.promotionIsValidated = true;
        const promotion = new PromotionItem();
        this.promotionService.getPromotionCode(this.promotionCode).subscribe(result => {
            this.promotionisValid = result.valid;
            this.promotionValidationResult = this.promotionisValid ? PromotionValidation.OK : PromotionValidation.InvalidCode;
            promotion.discount = result.code.discount;
            promotion.label = result.code.label;
            promotion.offprice = result.code.offprice;
            promotion.useInConjuction = result.code.useInConjuction;
            const canAddCodes = this.promotions.filter(x => x.useInConjuction === true).length === 0;
            if (!canAddCodes) {
                this.promotionisValid = false;
                this.promotionValidationResult = PromotionValidation.InvalidInConjuctionWithOtherCodes;
            } else {
                if (this.promotionisValid === true) {
                    this.promotionValidationResult = PromotionValidation.OK;
                    if (this.promotions.find(x => x.label === promotion.label)) {
                        this.promotionisValid = false;
                        this.promotionValidationResult = PromotionValidation.AllreadyAdded;
                    }
                    if (this.promotionisValid === true && promotion.useInConjuction === false) {
                        if (this.promotions.length > 0) {
                            this.promotionValidationResult = PromotionValidation.InvalidInConjuctionWithOtherCodes;
                            this.promotionisValid = false;
                        }
                    }
                } else {
                    this.promotionValidationResult = PromotionValidation.InvalidCode;
                }
            }
            this.promotionCode = undefined;
            if (this.promotionisValid === true && promotion) {
                this.store.dispatch(new ApplyPromotionAction(promotion));
                this.calculateSubtotal();
            }
        });
    }

    afterOrderCreated(): void {
        this.store.dispatch(new DeleteAllItemAction());
        this.store.dispatch(new DeleteAllPromotionItemsAction());
        this.isOrderCreated = true;
    }
}
