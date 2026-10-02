import { RestaurantMenuGetListModel } from '../restaurant-menu/restaurant-menu-get-list.model';

export class CategoryGetListModel {
  public categoryId: number = 0;
  public name: string = '';
  public isAvailable: boolean = false;
  public numberOfProducts: number = 0;
  public isSelected: boolean = false;
  public discount: number = 0;
  public discountUntil: Date | null = null;

  public menu: RestaurantMenuGetListModel[] = [];
}
