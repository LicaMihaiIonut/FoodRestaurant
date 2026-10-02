export class ScheduleAddEditModel {
  public scheduleId: number = 0;
  public start: Date = new Date();
  public end: Date = new Date();
  public dateTimeOffSet: number = 0;
  public isFreeDay: boolean = false;
}
