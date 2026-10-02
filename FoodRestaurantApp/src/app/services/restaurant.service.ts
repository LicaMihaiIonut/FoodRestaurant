import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RestaurantGetListModel } from '../models/restaurants/restaurant-get-list.model';
import { RestaurantProfileGetModel } from '../models/restaurants/restaurant-profile-get.model';
import { RestaurantProfilePostModel } from '../models/restaurants/restaurant-profile-post.model';
import { RestaurantGetModel } from '../models/restaurants/restaurant-get.model';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  public getAll(): Observable<RestaurantGetListModel[]> {
    return this.httpClient.get<RestaurantGetListModel[]>(
      `${this.baseUrl}restaurants`
    );
  }

  public get(restaurantId: number | null): Observable<RestaurantGetModel> {
    return this.httpClient.get<RestaurantGetModel>(
      `${this.baseUrl}restaurants/${restaurantId}`
    );
  }

  public getProfile(): Observable<RestaurantProfileGetModel> {
    return this.httpClient.get<RestaurantProfileGetModel>(
      `${this.baseUrl}restaurants/profile`
    );
  }

  public updateProfile(data: RestaurantProfilePostModel): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}restaurants/profile`, data);
  }
}
