import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout/layout.component';
import { CartDetailComponent } from './cart/cart-detail.component';

const routes: Routes = [
    { path: '', component: LayoutComponent },
    { path: 'cart-detail', component: CartDetailComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
