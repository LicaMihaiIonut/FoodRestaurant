import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { OrderDashboardModel } from '../models/orders/order-dashboard.model';
import { Observable } from 'rxjs';
import { OrderGetListModel } from '../models/orders/order-get-list.model';
import { PageResultModel } from '../models/page-result.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }
  public getAll(
    restaurantId: number,
    orderDashboardType: number
  ): Observable<OrderDashboardModel> {
    return this.httpClient.get<OrderDashboardModel>(
      `${this.baseUrl}orders/dashboard?restaurantId=${restaurantId}&orderDashboardType=${orderDashboardType}`
    );
  }

  public placeOrder(addressId: number, cardId: number): Observable<any> {
    return this.httpClient.post(
      `${this.baseUrl}orders/place?addressId=${addressId}&cardId=${cardId}`,
      {}
    );
  }

  public getAllForUser(): Observable<OrderGetListModel[]> {
    return this.httpClient.get<OrderGetListModel[]>(
      `${this.baseUrl}orders/user`
    );
  }

  public getAllForRestaurant(
    searchText: string,
    page: number
  ): Observable<PageResultModel<OrderGetListModel>> {
    return this.httpClient.get<PageResultModel<OrderGetListModel>>(
      `${this.baseUrl}orders/restaurant?searchText=${searchText}&page=${page}`
    );
  }

  public placeReview(data: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}orders/review`, data);
  }

  public changeStatus(
    orderId: number,
    status: number,
    deliveryTime: Date
  ): Observable<any> {
    return this.httpClient.post(
      `${this.baseUrl}orders/status?orderId=${orderId}&status=${status}&deliveryTime=${deliveryTime}`,
      {}
    );
  }
}
