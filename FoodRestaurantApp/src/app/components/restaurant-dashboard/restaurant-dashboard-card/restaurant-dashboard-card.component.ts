import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-restaurant-dashboard-card',
  templateUrl: './restaurant-dashboard-card.component.html',
  styleUrls: ['./restaurant-dashboard-card.component.css'],
})
export class RestaurantDashboardCardComponent implements OnInit {
  @Input() public value: number = 0;
  @Input() public roundValue: boolean = false;
  @Input() public percentage: number = 0;
  @Input() public title: string = '';
  @Input() public icon: string = '';

  constructor() {}

  ngOnInit(): void {}

  public getValue(): string {
    return this.roundValue ? this.value.toFixed(2) : this.value.toString();
  }

  public getDescription(): string {
    if (this.icon === 'bi bi-currency-exchange')
      return this.getRevenueDescription();

    if (this.icon === 'bi bi-cart-check-fill')
      return this.getOrderCountDescription();

    if (this.icon === 'bi bi-bar-chart-fill')
      return this.getAverageRevenueDescription();

    if (this.icon === 'bi bi-chat-dots-fill')
      return this.getReviewsDescription();

    return '';
  }

  private getRevenueDescription(): string {
    return `Total revenue generated with `;
  }

  private getOrderCountDescription(): string {
    return `Total orders completed with `;
  }

  private getAverageRevenueDescription(): string {
    return `Average revenue per order with `;
  }

  private getReviewsDescription(): string {
    return `Total reviews rate obtained with `;
  }

  public getTranslatedDescription(): string {
    let direction = '';

    if (this.percentage === 0) direction = 'no change from last month.';
    if (this.percentage > 0)
      direction = `${this.percentage.toFixed(2)}% increase from last month.`;
    if (this.percentage < 0)
      direction = `${-this.percentage.toFixed(2)}% decrease from last month.`;

    return direction;
  }
}
