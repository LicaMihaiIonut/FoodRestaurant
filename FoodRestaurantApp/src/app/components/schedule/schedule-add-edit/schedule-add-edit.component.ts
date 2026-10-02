import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ScheduleAddEditModel } from 'src/app/models/schedules/schedule-add-edit.model';
import { ScheduleGetListModel } from 'src/app/models/schedules/schedule-get-list.model';
import { ScheduleService } from 'src/app/services/schedule.service';

@Component({
  selector: 'app-schedule-add-edit',
  templateUrl: './schedule-add-edit.component.html',
  styleUrls: ['./schedule-add-edit.component.css'],
})
export class ScheduleAddEditComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<ScheduleAddEditComponent>,
    private formBuilder: FormBuilder,
    private scheduleService: ScheduleService,
    @Inject(MAT_DIALOG_DATA) public data: ScheduleGetListModel
  ) {}

  public ngOnInit(): void {}

  public form = this.formBuilder.group({
    start: new FormControl(
      { value: this.getTime(this.data?.start), disabled: this.data?.isFreeDay },
      Validators.required
    ),
    end: new FormControl(
      { value: this.getTime(this.data?.end), disabled: this.data?.isFreeDay },
      Validators.required
    ),
    isFreeDay: new FormControl(this.data?.isFreeDay),
  });

  public onSlideToggleChange() {
    const freeDay = this.form.controls['isFreeDay'].value;

    if (freeDay) {
      this.form.controls['start'].disable();
      this.form.controls['end'].disable();
    } else {
      this.form.controls['start'].enable();
      this.form.controls['end'].enable();
    }
  }

  public close(): void {
    this.dialogRef.close();
  }

  public submit(): void {
    console.log(this.form.controls['start'].value);
    if (!this.form.valid) {
      return;
    }

    const data = this.getData();
    if (!data.start || !data.end) {
      return;
    }

    this.send(data).subscribe(() => {
      this.close();
    });
  }

  public getTitle(): string {
    const datepipe: DatePipe = new DatePipe('en-US');
    const date = datepipe.transform(this.data.selectedDate, 'dd MMMM YYYY');

    const title = this.data?.scheduleId ? 'Edit' : 'Add';
    return `${title} schedule (${date})`;
  }

  private getData(): any {
    const startTime = this.form.controls['start'].value;
    const endTime = this.form.controls['end'].value;

    const startDate = new Date(this.data.selectedDate);
    startDate.setHours(startTime.substring(0, 2));
    startDate.setMinutes(startTime.substring(3, 5));

    const endDate = new Date(this.data.selectedDate);
    endDate.setHours(endTime.substring(0, 2));
    endDate.setMinutes(endTime.substring(3, 5));

    console.log(startDate);
    const data = {
      scheduleId: this.data?.scheduleId,
      start: startDate,
      end: endDate,
      dateTimeOffSet: 180,
      isFreeDay: this.form.controls['isFreeDay'].value,
    };

    return data;
  }

  private send(data: ScheduleAddEditModel): Observable<any> {
    return this.data?.scheduleId
      ? this.scheduleService.patch(data)
      : this.scheduleService.post(data);
  }

  private getTime(date: Date): string {
    const newDate = date ? new Date(date) : new Date();

    const hours = newDate.getHours();
    const minutes = newDate.getMinutes();
    const formattedHours = hours >= 10 ? `${hours}` : `0${hours}`;
    const formattedMinutes = minutes >= 10 ? `${minutes}` : `0${minutes}`;
    return `${formattedHours}:${formattedMinutes}`;
  }
}
