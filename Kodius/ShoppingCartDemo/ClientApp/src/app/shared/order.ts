import { ShoppingItem as ShoppingItem } from './shopping-item';

export class Order {
    id: number;
    orderTotal: number;
    products: ShoppingItem[];
    promotionCodes: string[];

    constructor() {
        this.id = undefined;
        this.products = [];
        this.promotionCodes = [];
        this.orderTotal = undefined;
    }
}
