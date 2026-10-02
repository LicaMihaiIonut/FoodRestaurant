export class RestaurantMenuGetListModel {
  public restaurantMenuId: number | undefined;
  public name: string | undefined;
  public price: number | undefined;
  public discount: number | undefined;
  public discountUntil: Date | undefined;
  public isSelected: boolean = false;
  public categoryName: string = '';
  public categoryId: number | undefined;
  public image: string = '';
}
