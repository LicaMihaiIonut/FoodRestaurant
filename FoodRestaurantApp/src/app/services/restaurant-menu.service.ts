import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RestaurantMenuGetListModel } from '../models/restaurant-menu/restaurant-menu-get-list.model';
import { RestaurantMenuPostModel } from '../models/restaurant-menu/restaurant-menu-post.model';
import { PageResultModel } from '../models/page-result.model';

@Injectable({
  providedIn: 'root',
})
export class RestaurantMenuService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  public getAll(
    searchText: string,
    currentPage: number
  ): Observable<PageResultModel<RestaurantMenuGetListModel>> {
    return this.httpClient.get<PageResultModel<RestaurantMenuGetListModel>>(
      `${this.baseUrl}restaurantmenus?searchText=${searchText}&currentPage=${currentPage}`
    );
  }

  public post(data: RestaurantMenuPostModel) {
    return this.httpClient.post(`${this.baseUrl}restaurantmenus/add`, data);
  }

  public remove(data: (number | undefined)[]) {
    return this.httpClient.post(`${this.baseUrl}restaurantmenus/delete`, data);
  }

  public uploadPicture(data: any) {
    return this.httpClient.post(
      `${this.baseUrl}restaurantmenus/upload-image`,
      data
    );
  }
}
