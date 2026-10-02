import { Component, OnInit } from '@angular/core';
import { RestaurantProfileGetModel } from 'src/app/models/restaurants/restaurant-profile-get.model';
import { RestaurantService } from 'src/app/services/restaurant.service';

@Component({
  selector: 'app-restaurant-profile',
  templateUrl: './restaurant-profile.component.html',
  styleUrls: ['./restaurant-profile.component.css'],
})
export class RestaurantProfileComponent implements OnInit {
  public activeTab: string = 'Profile';

  private localStorageTabActiveKey: string = 'restaurantProfileTabActive';

  constructor() {}

  public ngOnInit(): void {
    this.activeTab =
      localStorage.getItem(this.localStorageTabActiveKey) ?? 'Profile';
  }

  public isTabActive(tab: string): boolean {
    return this.activeTab === tab;
  }

  public setTabActive(tab: string): void {
    this.activeTab = tab;
    localStorage.setItem(this.localStorageTabActiveKey, this.activeTab);
  }
}
