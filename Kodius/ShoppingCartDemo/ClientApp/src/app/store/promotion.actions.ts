import { Action } from '@ngrx/store';
import { PromotionItem } from '../shared/promotion-item';

export enum PromotionActionTypes {
    DELETE_PROMOTION = '[PROMOTION] Delete Promotion',
    DELETE_All_PROMOTIONS = '[PROMOTION] Delete All Promotions',
    APPLY_PROMOTION = '[PROMOTION] Apply Promotion'
}
// Delete Single
export class DeletePromotionAction implements Action {
    readonly type = PromotionActionTypes.DELETE_PROMOTION;
    constructor(public payload: string) { }
}
// Delete All
export class DeleteAllPromotionItemsAction implements Action {
    readonly type = PromotionActionTypes.DELETE_All_PROMOTIONS;
    constructor() { }
}
// Apply promotion
export class ApplyPromotionAction implements Action {
    readonly type = PromotionActionTypes.APPLY_PROMOTION;
    constructor(public payload: PromotionItem) { }
}


export type PromotionAction =
ApplyPromotionAction |
    DeletePromotionAction |
    DeleteAllPromotionItemsAction;
