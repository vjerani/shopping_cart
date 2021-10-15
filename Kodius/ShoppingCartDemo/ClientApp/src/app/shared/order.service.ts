import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Order } from './order';

export class OrderInput {
    promotionCodes: string[];
    products: OrderItem[];
    constructor() {
        this.products = [];
        this.promotionCodes = [];
    }
}

export class OrderItem {
    productId: number;
    quantity: number;
    constructor() {
        this.productId = undefined;
        this.quantity = undefined;
    }
}

export class OrderOutput {
    id: number;
}

@Injectable({ providedIn: 'root' })
export class OrderService {
    private url = 'https://localhost:44387/api/cart/checkout';
    private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
    private httpOptions = { headers: this.httpHeaders };

    constructor(private httpClient: HttpClient) { }

    add(order: Order): Observable<OrderOutput> {
        const orderInput: OrderInput = new OrderInput();
        orderInput.promotionCodes = order.promotionCodes;
        orderInput.products = order.products.map( x => {
            return { productId: x.product.productId, quantity: x.quantity } as OrderItem;
        });
        return this.httpClient.post<OrderOutput>(this.url, orderInput, this.httpOptions)
            .pipe(
                catchError(this.handleError)
            );
    }

    private handleError(err) {
        let errorMessage: string;
        if (err.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            errorMessage = `An error occurred: ${err.error.message}`;
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
        }
        console.error(err);
        return throwError(errorMessage);
    }

}
