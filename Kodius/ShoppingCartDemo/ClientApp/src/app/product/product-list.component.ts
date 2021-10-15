import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { v4 as uuid } from 'uuid';

import { Product } from '../shared/product';
import { ShoppingItem } from '../shared/shopping-item';
import { AppState } from '../store/app-state';
import { AddItemAction } from '../store/shopping.actions';
import { ProductService } from '../shared/product.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-product-list',
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
    products: Product[] = [];

    constructor(private store: Store<AppState>,
                private productService: ProductService) { }

    ngOnInit() {
        this.productService.getAll().subscribe(result => {
            this.products = result.products;
            console.log(result);
        });
    }

    addToCart(selectedProduct: Product): void {
        const newShoppingItem = {
            id: selectedProduct.productId,
            quantity: 1,
            price: selectedProduct.productPrice,
            product: selectedProduct
        } as ShoppingItem;

        this.store.dispatch(new AddItemAction(newShoppingItem));
    }
}
