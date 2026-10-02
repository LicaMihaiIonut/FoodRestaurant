import { Component, OnInit } from '@angular/core';
import { RestaurantProfileGetModel } from 'src/app/models/restaurants/restaurant-profile-get.model';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-restaurant-administration',
  templateUrl: './restaurant-administration.component.html',
  styleUrls: ['./restaurant-administration.component.css'],
})
export class RestaurantAdministrationComponent implements OnInit {
  public menuItemActive: string = '';

  constructor(
    private userService: UserService,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    const menuItemCached = localStorage.getItem('restaurantAdministrationMenu');
    if (menuItemCached) {
      this.menuItemActive = menuItemCached;
    }

    if (this.restaurantImage == null && this.restaurantName == null) {
      this.restaurantService
        .getProfile()
        .subscribe((response: RestaurantProfileGetModel) => {
          localStorage.setItem('restaurant-image', response.image ?? '');
          localStorage.setItem('restaurant-name', response.name ?? '');
        });
    }
  }

  public get restaurantImage(): string | null {
    return localStorage.getItem('restaurant-image');
  }

  public get restaurantName(): string | null {
    return localStorage.getItem('restaurant-name');
  }

  public setMenuItemActive(menuItem: string): void {
    this.menuItemActive = menuItem;
    localStorage.setItem('restaurantAdministrationMenu', this.menuItemActive);
  }

  public isMenuItemActive(menuItem: string): boolean {
    return this.menuItemActive === menuItem;
  }

  public logout(): void {
    this.userService.logout();
  }
}
