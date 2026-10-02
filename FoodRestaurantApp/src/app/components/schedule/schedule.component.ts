import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ScheduleGetListModel } from 'src/app/models/schedules/schedule-get-list.model';
import { ScheduleService } from 'src/app/services/schedule.service';
import { ScheduleAddEditComponent } from './schedule-add-edit/schedule-add-edit.component';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
})
export class ScheduleComponent implements OnInit {
  public dayNames: string[] = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ];
  public calendarDates: Map<string, Date[]> = new Map<string, Date[]>();

  public currentMonth: number = 0;
  public currentDay: number = 0;
  public currentYear: number = 0;

  public calendarRows: number = 0;

  public schedules: ScheduleGetListModel[] = [];

  constructor(
    private scheduleService: ScheduleService,
    public dialog: MatDialog
  ) {}

  public ngOnInit(): void {
    const date = new Date();

    this.currentDay = date.getDate();
    this.currentMonth = date.getMonth();
    this.currentYear = date.getFullYear();

    this.initialize();
  }

  public initialize(): void {
    this.initializeCalendarDays();
    this.addCurentMonthDates();
    this.addPreviousMonthDates();
    this.addNextMonthDates();

    this.calendarRows = this.calendarDates.get('Monday')?.length ?? 0;

    this.loadSchedules();
  }

  public loadSchedules(): void {
    this.scheduleService
      .getAll(this.currentMonth + 1, this.currentYear)
      .subscribe((response: ScheduleGetListModel[]) => {
        this.schedules = response;
        console.log(response);
      });
  }

  public hasSchedule(row: number, col: number): boolean {
    const date = this.getDate(row, col);
    const schedule = this.getSchedule(date);

    return !!schedule;
  }

  public isFreeDay(row: number, col: number): boolean {
    const date = this.getDate(row, col);
    const schedule = this.getSchedule(date);

    return schedule?.isFreeDay ?? false;
  }

  public getFormattedSchedule(row: number, col: number): string {
    const date = this.getDate(row, col);
    const schedule = this.getSchedule(date);

    const start = new Date(schedule.start);
    const startHour = start.toLocaleString('en-US', {
      hour: 'numeric',
      hour12: true,
      minute: 'numeric',
    });

    const end = new Date(schedule.end);
    const endHour = end.toLocaleString('en-US', {
      hour: 'numeric',
      hour12: true,
      minute: 'numeric',
    });

    return `${startHour} - ${endHour}`;
  }

  public openSchedulePopup(row: number, col: number): void {
    const date = this.getDate(row, col);
    let schedule = this.getSchedule(date);
    if (!schedule) {
      schedule = new ScheduleGetListModel();
    }
    schedule.selectedDate = date;

    const dialogRef = this.dialog.open(ScheduleAddEditComponent, {
      width: '500px',
      data: schedule,
    });

    dialogRef.afterClosed().subscribe((_) => {
      this.initialize();
    });
  }

  public goToNextMonth(): void {
    const nextMonth = this.currentMonth === 11 ? 0 : this.currentMonth + 1;
    const year =
      this.currentMonth === 11 ? this.currentYear + 1 : this.currentYear;

    this.currentMonth = nextMonth;
    this.currentYear = year;

    this.initialize();
  }

  public goToPreviousMonth(): void {
    const previousMonth = this.currentMonth === 0 ? 11 : this.currentMonth - 1;
    const year =
      this.currentMonth === 0 ? this.currentYear - 1 : this.currentYear;

    this.currentMonth = previousMonth;
    this.currentYear = year;

    this.initialize();
  }

  public getDateDay(row: number, col: number): number {
    const date = this.getDate(row, col);
    return date.getDate();
  }

  public getWeekNumber(row: number): string | null {
    const dates = this.calendarDates.get('Sunday') as Date[];
    const date = dates[row];

    const datePipe = new DatePipe('en-US');
    const weekNumber = datePipe.transform(date, 'w');

    return weekNumber;
  }

  public isCurrentMonth(row: number, col: number): boolean {
    const date = this.getDate(row, col);
    return date.getMonth() === this.currentMonth;
  }

  public getMonthName(): string {
    const date = new Date(this.currentYear, this.currentMonth, 1);
    return date.toLocaleString('default', { month: 'long' });
  }

  private initializeCalendarDays(): void {
    this.dayNames.map((dayName: string) => {
      this.calendarDates.set(dayName, []);
    });
  }

  private addCurentMonthDates(): void {
    const firstDate = new Date(this.currentYear, this.currentMonth, 1);
    const lastDate = new Date(this.currentYear, this.currentMonth + 1, 0);
    console.log(lastDate);

    for (
      let index = firstDate.getDate();
      index <= lastDate.getDate();
      index++
    ) {
      const date = new Date(this.currentYear, this.currentMonth, index);

      this.addCalendarDate(date);
    }

    console.log(this.calendarDates);
  }

  private addPreviousMonthDates(): void {
    const firstDate = new Date(this.currentYear, this.currentMonth, 1);
    const dayName = firstDate.toLocaleDateString('en-US', {
      weekday: 'long',
    });

    const previousMonth = this.currentMonth === 0 ? 11 : this.currentMonth - 1;
    const year =
      this.currentMonth === 0 ? this.currentYear - 1 : this.currentYear;
    const lastDate = new Date(year, previousMonth + 1, 0);

    for (let index = 0; index < this.dayNames.indexOf(dayName); index++) {
      const date = new Date(year, previousMonth, lastDate.getDate() - index);

      this.addCalendarDate(date, false);
    }
  }

  private addNextMonthDates(): void {
    const lastDate = new Date(this.currentYear, this.currentMonth + 1, 0);
    const dayName = lastDate.toLocaleDateString('en-US', {
      weekday: 'long',
    });

    const nextMonth = this.currentMonth === 11 ? 0 : this.currentMonth + 1;
    const year =
      this.currentMonth === 11 ? this.currentYear + 1 : this.currentYear;
    const firstDate = new Date(year, nextMonth, 1);

    for (
      let index = 0;
      index < this.dayNames.length - this.dayNames.indexOf(dayName) - 1;
      index++
    ) {
      const date = new Date(year, nextMonth, firstDate.getDate() + index);

      this.addCalendarDate(date);
    }
  }

  private addCalendarDate(date: Date, insertAtLast: boolean = true): void {
    const dayName = date.toLocaleDateString('en-US', {
      weekday: 'long',
    });

    let days = this.calendarDates.get(dayName) as Date[];

    if (!days) {
      return;
    }

    insertAtLast ? days.push(date) : days.unshift(date);
    this.calendarDates.set(dayName, days);
  }

  private getDate(row: number, col: number): Date {
    const dayName = this.dayNames.filter(
      (x) => this.dayNames.indexOf(x) === row
    )[0];

    const days = this.calendarDates.get(dayName);
    return days ? days[col] : new Date();
  }

  private getSchedule(date: Date): ScheduleGetListModel {
    const schedules = this.schedules.filter((x) => {
      const startDate = new Date(x.start);
      return startDate.getDate() === date.getDate();
    });
    return schedules[0];
  }
}
