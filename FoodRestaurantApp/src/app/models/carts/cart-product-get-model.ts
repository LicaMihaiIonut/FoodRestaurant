export class CartProductGetModel {
  public cartProductId: number;
  public productId: number;
  public price: number;
  public discount: number | undefined;
  public description: string;
  public image: string;
  public quantity: number;
  public name: string;
}
