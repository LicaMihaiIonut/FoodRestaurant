import { ScheduleGetListModel } from '../schedules/schedule-get-list.model';

export class RestaurantGetListModel {
  public restaurantId: number | undefined;
  public name: string | undefined;
  public image: string | undefined;
  public transport: string | undefined;
  public delivery: string | undefined;
  public averageGrade: number;
  public totalReviewNumber: number;
  public schedule: ScheduleGetListModel;
}
