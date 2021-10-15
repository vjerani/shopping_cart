import { Action } from '@ngrx/store';
import { ShoppingItem } from '../shared/shopping-item';
import { PromotionCode } from '../shared/promotion.service';

export enum ShoppingActionTypes {
    ADD_ITEM = '[SHOPPING] Add Item',
    DELETE_ITEM = '[SHOPPING] Delete Item',
    DELETE_All_ITEM = '[SHOPPING] Delete All Item',
    UPDATE_ITEM = '[SHOPPING] Update Item',
    APPLY_PROMOTION = '[SHOPPING] Apply promotion'
}

// Add
export class AddItemAction implements Action {
    readonly type = ShoppingActionTypes.ADD_ITEM;
    constructor(public payload: ShoppingItem) { }
}
// Delete Single
export class DeleteItemAction implements Action {
    readonly type = ShoppingActionTypes.DELETE_ITEM;
    constructor(public payload: number) { }
}
// Delete All
export class DeleteAllItemAction implements Action {
    readonly type = ShoppingActionTypes.DELETE_All_ITEM;
    constructor() { }
}
// Update
export class UpdateItemAction implements Action {
    readonly type = ShoppingActionTypes.UPDATE_ITEM;
    constructor(public payload: ShoppingItem) { }
}
// Apply promotion
export class ApplyPromotionAction implements Action {
    readonly type = ShoppingActionTypes.APPLY_PROMOTION;
    constructor(public payload: PromotionCode) { }
}


export type ShoppingAction =
    AddItemAction |
    DeleteItemAction |
    DeleteAllItemAction |
    UpdateItemAction;
