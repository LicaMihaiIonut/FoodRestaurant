import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { OrderGetListModel } from 'src/app/models/orders/order-get-list.model';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-restaurant-order-info',
  templateUrl: './restaurant-order-info.component.html',
  styleUrls: ['./restaurant-order-info.component.css'],
})
export class RestaurantOrderInfoComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<RestaurantOrderInfoComponent>,
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    @Inject(MAT_DIALOG_DATA) public data: OrderGetListModel
  ) {}

  public form = this.formBuilder.group({
    status: new FormControl(this.data.status - 1, [Validators.required]),
    deliveryTime: new FormControl(this.getTime(this.data.deliveryTime), [
      Validators.required,
    ]),
  });

  public isLoading: boolean = false;

  public statusses: string[] = ['New', 'Preparing', 'Delivering', 'Delivered'];

  ngOnInit(): void {}

  public close(): void {
    this.dialogRef.close();
  }

  public submit(): void {
    if (!this.form.valid) {
      return;
    }

    const status = this.form.controls['status'].value + 1;
    const deliveryTime = this.form.controls['deliveryTime'].value;
    console.log(deliveryTime);

    this.orderService
      .changeStatus(this.data.orderId, status, deliveryTime)
      .subscribe((_: any) => {
        this.dialogRef.close({ status: status, deliveryTime: deliveryTime });
      });
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
