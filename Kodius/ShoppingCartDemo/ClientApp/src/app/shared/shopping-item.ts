import { Product } from './product';

export interface ShoppingItem {
    id: number;
    quantity: number;
    price: number;
    product: Product;
}
