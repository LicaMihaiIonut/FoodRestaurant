import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { ReviewGetListModel } from '../models/reviews/review-get-list.model';
import { Observable } from 'rxjs';
import { PageResultModel } from '../models/page-result.model';
import { ReviewStatisticsModel } from '../models/reviews/review-statistics.model';

@Injectable({
  providedIn: 'root',
})
export class ReviewService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  public getAll(
    searchText: string,
    currentPage: number
  ): Observable<PageResultModel<ReviewGetListModel>> {
    return this.httpClient.get<PageResultModel<ReviewGetListModel>>(
      `${this.baseUrl}reviews?searchText=${searchText}&currentPage=${currentPage}`
    );
  }

  public getStatistics(): Observable<ReviewStatisticsModel> {
    return this.httpClient.get<ReviewStatisticsModel>(
      `${this.baseUrl}reviews/statistics`
    );
  }
}
