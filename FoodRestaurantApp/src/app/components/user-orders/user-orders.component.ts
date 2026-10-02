import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { OrderGetListModel } from 'src/app/models/orders/order-get-list.model';
import { ReviewGetListModel } from 'src/app/models/reviews/review-get-list.model';
import { OrderService } from 'src/app/services/order.service';
import { ReviewDetailsComponent } from '../restaurant-profile/review-details/review-details.component';
import { MatDialog } from '@angular/material/dialog';
import { ReviewAddComponent } from '../restaurant-profile/review-add/review-add.component';

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.css'],
})
export class UserOrdersComponent implements OnInit {
  public orders: OrderGetListModel[] = [];

  constructor(private orderService: OrderService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.orderService
      .getAllForUser()
      .subscribe((response: OrderGetListModel[]) => {
        this.orders = response;
      });
  }

  public toggle(order: OrderGetListModel, toggle: boolean): void {
    order.toggled = toggle;
  }

  public getCreatedOn(date: Date | undefined): string | null {
    const datePipe = new DatePipe('en-US');
    return datePipe.transform(date, 'short');
  }

  public getStatus(status: number): string {
    if (status === 1) {
      return 'Waiting for restaurant to pick up the order...';
    }

    if (status === 2) {
      return 'Preparing...';
    }

    if (status === 3) {
      return 'Your order is delivered...';
    }

    return 'Delivered';
  }

  public hasReview(review: ReviewGetListModel): boolean {
    return !!review && review.grade !== -1;
  }

  public addReview(orderId: number): void {
    const ref = this.dialog.open(ReviewAddComponent, {
      width: '500px',
      data: { orderId: orderId },
    });

    ref.afterClosed().subscribe((response: any) => {
      if (response) {
        const order = this.orders.find(
          (x: OrderGetListModel) => x.orderId === orderId
        );

        if (order && order.review) {
          order.review.description = response.description;
          order.review.grade = response.grade;
        }
      }
    });
  }
}
