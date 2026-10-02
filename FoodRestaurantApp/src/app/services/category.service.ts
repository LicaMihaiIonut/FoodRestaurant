import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PageResultModel } from '../models/page-result.model';
import { CategoryGetListModel } from '../models/categories/category-get-list.model';
import { CategoryAddEditModel } from '../models/categories/category-add-edit.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  public getAll(
    searchText: string,
    currentPage: number | null
  ): Observable<PageResultModel<CategoryGetListModel>> {
    return this.httpClient.get<PageResultModel<CategoryGetListModel>>(
      `${this.baseUrl}categories?searchText=${searchText}&currentPage=${currentPage}`
    );
  }

  public post(data: CategoryAddEditModel) {
    return this.httpClient.post(`${this.baseUrl}categories/add`, data);
  }

  public patch(data: CategoryAddEditModel) {
    return this.httpClient.patch(`${this.baseUrl}categories/patch`, data);
  }

  public remove(data: (number | undefined)[]) {
    return this.httpClient.post(`${this.baseUrl}categories/delete`, data);
  }
}
