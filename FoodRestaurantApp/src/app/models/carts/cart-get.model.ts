import { CartProductGetModel } from './cart-product-get-model';

export class CartGetModel {
  public totalPrice: number;
  public transport: number;
  public products: CartProductGetModel[] = [];

  constructor() {}
}
