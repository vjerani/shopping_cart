import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export class PromotionCode {
    public label: string;
    public discount: number;
    public offprice: number;
    public useInConjuction: boolean;
    constructor() {
        this.discount = 0;
        this.label = undefined;
        this.offprice = 0;
        this.useInConjuction = true;
    }
}

export class PromotionCodeOutput {
    public valid: boolean;
    public code: PromotionCode;
    constructor() {
        this.valid = false;
        this.code = undefined;
    }
}

@Injectable({ providedIn: 'root' })
export class PromotionService {
    private url = 'https://localhost:44387/api/cart/promotion';
    private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
    private httpOptions = { headers: this.httpHeaders };

    constructor(private httpClient: HttpClient) { }

    getPromotionCode(code: string): Observable<PromotionCodeOutput> {
        return this.httpClient.get<PromotionCodeOutput>(this.url, {
            params: new HttpParams().set('code', code)
        })
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
