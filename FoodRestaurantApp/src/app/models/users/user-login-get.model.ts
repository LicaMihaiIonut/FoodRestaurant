export class UserLoginGetModel {
  public userId: number = 0;
  public restaurantId: number | null = null;
  public cartId: number | null = null;
  public token: string = '';
  public email: string = '';
  public name: string = '';
  public numberOfProducts: number;
}
