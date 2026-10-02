import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { ScheduleGetListModel } from '../models/schedules/schedule-get-list.model';
import { Observable } from 'rxjs';
import { ScheduleAddEditModel } from '../models/schedules/schedule-add-edit.model';

@Injectable({
  providedIn: 'root',
})
export class ScheduleService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  public getAll(
    month: number,
    year: number
  ): Observable<ScheduleGetListModel[]> {
    return this.httpClient.get<ScheduleGetListModel[]>(
      `${this.baseUrl}schedules?month=${month}&year=${year}`
    );
  }

  public post(data: ScheduleAddEditModel) {
    return this.httpClient.post(`${this.baseUrl}schedules/add`, data);
  }

  public patch(data: ScheduleAddEditModel) {
    return this.httpClient.patch(`${this.baseUrl}schedules/patch`, data);
  }

  public remove(scheduleId: number) {
    return this.httpClient.delete(
      `${this.baseUrl}schedules/delete?scheduleId=${scheduleId}`
    );
  }
}
