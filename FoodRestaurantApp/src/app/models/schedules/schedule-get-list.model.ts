export class ScheduleGetListModel {
  public scheduleId: number = 0;
  public start: Date = new Date();
  public end: Date = new Date();
  public selectedDate: Date = new Date();
  public isFreeDay: boolean = false;
}
