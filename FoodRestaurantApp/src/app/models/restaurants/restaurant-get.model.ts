import { CategoryGetListModel } from '../categories/category-get-list.model';

export class RestaurantGetModel {
  public image: string = '';
  public name: string = '';
  public delivery: string = '';
  public transport: string = '';
  public categories: CategoryGetListModel[] = [];
}
