import { PromotionItem } from '../shared/promotion-item';
import { PromotionAction, PromotionActionTypes } from './promotion.actions';


const initialState: PromotionItem[] = [];

export function PromotionReducer(state: PromotionItem[] = initialState, action: PromotionAction) {
    switch (action.type) {
        // Add
        case PromotionActionTypes.APPLY_PROMOTION:
            return [...state, action.payload];
        // Delete Single
        case PromotionActionTypes.DELETE_PROMOTION:
            return state.filter(item => item.label !== action.payload);
        // Delete All
        case PromotionActionTypes.DELETE_All_PROMOTIONS:
            return [];
        default:
            return state;
    }
}
