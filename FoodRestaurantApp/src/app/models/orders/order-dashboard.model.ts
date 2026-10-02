import { OrderDashboardDataModel } from './order-dashboard-data.model';

export class OrderDashboardModel {
  public revenue: number = 0;
  public revenuePercentage: number = 0;

  public orderCount: number = 0;
  public orderCountPercentage: number = 0;

  public averageRevenue: number = 0;
  public averageRevenuePercentage: number = 0;

  public reviews: number = 0;
  public reviewsPercentage: number = 0;

  public data: OrderDashboardDataModel[] = [];
}
