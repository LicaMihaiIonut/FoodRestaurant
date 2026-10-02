import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OrderGetListModel } from 'src/app/models/orders/order-get-list.model';
import { PageResultModel } from 'src/app/models/page-result.model';
import { OrderService } from 'src/app/services/order.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { RestaurantOrderInfoComponent } from './restaurant-order-info/restaurant-order-info.component';

@Component({
  selector: 'app-restaurant-orders',
  templateUrl: './restaurant-orders.component.html',
  styleUrls: ['./restaurant-orders.component.css'],
})
export class RestaurantOrdersComponent implements OnInit {
  private _searchText: string = '';
  private _currentPage: number = 1;

  public orders: OrderGetListModel[] = [];
  public nrOfRecords: number = 0;
  constructor(
    private orderService: OrderService,
    private snackbarService: SnackbarService,
    private matDialog: MatDialog
  ) {}

  public ngOnInit(): void {
    this.load();
  }

  public viewManagementChanged($event: any): void {
    this._searchText = $event.searchText;
    this._currentPage = $event.currentPage;

    this.load();
  }

  public load(): void {
    this.orderService
      .getAllForRestaurant(this._searchText, this._currentPage)
      .subscribe((response: PageResultModel<OrderGetListModel>) => {
        this.orders = response.entities;
        this.nrOfRecords = response.numberOfRecords;
      });
  }

  public getDate(date: Date): string | null {
    const datePipe = new DatePipe('en-US');
    return datePipe.transform(date, 'short');
  }

  public getStatus(order: OrderGetListModel): string {
    if (order.status === 1) {
      return 'New';
    }

    if (order.status === 2) {
      return 'Preparing';
    }

    if (order.status === 3) {
      return 'Delivering';
    }

    return 'Delivered';
  }

  public openInfo(order: OrderGetListModel): void {
    const ref = this.matDialog.open(RestaurantOrderInfoComponent, {
      width: '500px',
      data: order,
    });

    ref.afterClosed().subscribe((response: any) => {
      if (response) {
        order.status = response.status;
        order.deliveryTime = new Date(response.deliveryTime);
      }
    });
  }
}
