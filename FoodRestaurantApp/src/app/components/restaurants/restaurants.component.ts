import { Component, OnInit } from '@angular/core';
import { RestaurantGetListModel } from 'src/app/models/restaurants/restaurant-get-list.model';
import { ScheduleGetListModel } from 'src/app/models/schedules/schedule-get-list.model';
import { RestaurantService } from 'src/app/services/restaurant.service';

@Component({
  selector: 'app-restaurants',
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css'],
})
export class RestaurantsComponent implements OnInit {
  public restaurants: RestaurantGetListModel[] = [];
  public originalRestaurants: RestaurantGetListModel[] = [];

  constructor(private restaurantService: RestaurantService) {}

  ngOnInit(): void {
    this.restaurantService.getAll().subscribe((data) => {
      this.restaurants = data;
      this.originalRestaurants = this.restaurants.slice(0);
    });
  }

public filterRestaurants(name:any){
  console.log('in restaurants:' +name);
  this.restaurants = this.originalRestaurants.filter((restaurant) => restaurant.name?.toUpperCase().startsWith(name.toUpperCase()));
}

  public closed(schedule: ScheduleGetListModel): boolean {
    const currentDate = new Date();
    const scheduleEndDate = new Date(schedule?.end);
    const scheduleStartDate = new Date(schedule?.start);
    const freeDay = schedule?.isFreeDay;

    if (!schedule) {
      return true;
    }

    return (
      currentDate.getTime() > scheduleEndDate.getTime() ||
      currentDate.getTime() < scheduleStartDate.getTime() ||
      freeDay
    );
  }
}
