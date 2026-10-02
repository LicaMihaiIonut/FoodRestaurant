import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Output, EventEmitter } from '@angular/core'; 
@Component({
  selector: 'app-restaurant-header',
  templateUrl: './restaurant-header.component.html',
  styleUrls: ['./restaurant-header.component.css'],
})
export class RestaurantHeaderComponent implements OnInit {
  public searchText: string = '';

  constructor(private router: Router) {}

  ngOnInit(): void {}

  @Output() someEvent = new EventEmitter<string>();

  public search(): void {
    console.log(this.searchText);
    this.someEvent.next(this.searchText);
  }

  public goBack(): void {
    this.router.navigateByUrl('restaurants');
  }

  public get CartCount(): number {
    const count = localStorage.getItem('cartCount');

    if (count == null) {
      return 0;
    }

    return Number(count) ?? 0;
  }
}
