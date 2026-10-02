import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { CartPostModel } from '../models/carts/cart-post.model';
import { CartProductCountModel } from '../models/carts/cart-product-count.model';
import { Observable } from 'rxjs';
import { CartGetModel } from '../models/carts/cart-get.model';

@Injectable({
  providedIn: 'root',
})
export class CartService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  public get(): Observable<CartGetModel> {
    return this.httpClient.get<CartGetModel>(`${this.baseUrl}cart`);
  }

  public post(data: CartPostModel): Observable<CartProductCountModel> {
    return this.httpClient.post<CartProductCountModel>(
      `${this.baseUrl}cart/post`,
      data
    );
  }

  public delete(cartProductId: number): Observable<any> {
    return this.httpClient.delete(
      `${this.baseUrl}cart/delete/${cartProductId}`
    );
  }
}
