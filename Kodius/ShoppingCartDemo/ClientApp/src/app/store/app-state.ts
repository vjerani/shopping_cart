import { ShoppingItem } from '../shared/shopping-item';
import { PromotionItem } from '../shared/promotion-item';

export interface AppState {
  readonly shopping: ShoppingItem[];
  readonly promotions: PromotionItem[];
}
