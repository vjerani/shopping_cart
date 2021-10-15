import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from './product';

export class ProductsOutput {
    public products: Product[];
    constructor() {
        this.products = [];
    }
}

@Injectable({ providedIn: 'root' })
export class ProductService {
    private url = 'https://localhost:44387/api/products/all';
    private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
    private httpOptions = { headers: this.httpHeaders };

    constructor(private httpClient: HttpClient) { }

    getAll(): Observable<ProductsOutput> {
        return this.httpClient.get<ProductsOutput>(this.url)
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
