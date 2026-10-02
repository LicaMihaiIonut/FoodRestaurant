import { ReviewGetListModel } from '../reviews/review-get-list.model';

export class OrderGetListModel {
  public orderId: number;
  public totalPrice: number;
  public restaurantName: string;
  public restaurantId: number;
  public createdOn: Date;
  public address: string;
  public card: string;
  public status: number;
  public userName: string;
  public phone: string;
  public deliveryTime: Date;
  public toggled: boolean = false;

  public review: ReviewGetListModel;
  public products: any[] = [];
}
