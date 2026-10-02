import { Component, OnInit } from '@angular/core';
import { Chart } from 'angular-highcharts';
import { OrderDashboardDataModel } from 'src/app/models/orders/order-dashboard-data.model';
import { OrderDashboardModel } from 'src/app/models/orders/order-dashboard.model';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-restaurant-dashboard',
  templateUrl: './restaurant-dashboard.component.html',
  styleUrls: ['./restaurant-dashboard.component.css'],
})
export class RestaurantDashboardComponent implements OnInit {
  public filter: string = 'Month';
  public orderDashboard: OrderDashboardModel = new OrderDashboardModel();
  public chart: Chart = new Chart({});

  public isLoaded: boolean = false;

  private chartvalues: number[] = [];
  private chartColumns: string[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.load();
  }

  public load(): void {
    this.isLoaded = false;

    this.chartColumns = [];
    this.chartvalues = [];

    this.orderService
      .getAll(2, this.getActiveFilterId())
      .subscribe((response: OrderDashboardModel) => {
        this.orderDashboard = response;

        for (const data of this.orderDashboard.data) {
          this.chartvalues.push(Math.trunc(data.value));
          this.chartColumns.push(data.columnName);
        }

        this.isLoaded = true;

        this.createChart();
      });
  }

  public setFilter(filter: string): void {
    this.filter = filter;

    this.load();
  }

  public isFilterActive(filter: string): boolean {
    return this.filter === filter;
  }

  private createChart(): void {
    this.chart = new Chart({
      chart: {
        type: 'line',
      },
      title: {
        text: '',
      },
      credits: {
        enabled: false,
      },
      xAxis: {
        categories: this.chartColumns,
      },
      series: [
        {
          name: '',
          type: 'column',
          color: '#673ab7',
          data: this.chartvalues,
        },
      ],
    });
  }

  private getActiveFilterId(): number {
    if (this.filter === 'Month') return 1;
    if (this.filter === 'Quarter') return 2;

    return 3;
  }
}
