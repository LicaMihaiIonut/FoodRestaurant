import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartPostModel } from 'src/app/models/carts/cart-post.model';
import { CartProductCountModel } from 'src/app/models/carts/cart-product-count.model';
import { CategoryGetListModel } from 'src/app/models/categories/category-get-list.model';
import { RestaurantMenuGetListModel } from 'src/app/models/restaurant-menu/restaurant-menu-get-list.model';
import { RestaurantGetModel } from 'src/app/models/restaurants/restaurant-get.model';
import { CartService } from 'src/app/services/cart.service';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-restaurant-detail',
  templateUrl: './restaurant-detail.component.html',
  styleUrls: ['./restaurant-detail.component.css'],
})
export class RestaurantDetailComponent implements OnInit {
  private restaurantId: number | null = 0;
  private selectedCategoryId: number | null = -1;

  public selectedServiceId: number | undefined = -1;
  public stickyNavbar: boolean = false;

  public restaurant: RestaurantGetModel = new RestaurantGetModel();

  @ViewChild('categories-names') categoriesView: ElementRef;

  constructor(
    private restaurantService: RestaurantService,
    private activatedRoute: ActivatedRoute,
    private cartService: CartService,
    private snackBarService: SnackbarService
  ) {}

  public scrollBy(test: HTMLElement, scrollBy: number): void {
    test.scrollBy({ left: scrollBy, top: 0, behavior: 'smooth' });
  }

  public scroll = (_: any) => {
    this.stickyNavbar = window.scrollY > 100;
  };

  ngOnInit(): void {
    document.addEventListener('scroll', this.scroll, true);

    this.activatedRoute.params.subscribe((params: any) => {
      this.restaurantId = params['restaurantId'];

      this.restaurantService
        .get(this.restaurantId)
        .subscribe((response: RestaurantGetModel) => {
          this.restaurant = response;

          if (this.restaurant.categories?.length > 0) {
            this.selectedCategoryId = this.restaurant.categories[0].categoryId;
          }
        });
    });
  }

  public selectCategory(categoryId: number | null): void {
    this.selectedCategoryId = categoryId;

    const element = document.getElementById(`category-${categoryId}`);
    console.log(element?.scrollHeight);
    element?.scrollIntoView({ behavior: 'smooth' });
  }

  public isActive(categoryId: number | null): boolean {
    return this.selectedCategoryId === categoryId;
  }

  public hasDiscount(service: RestaurantMenuGetListModel) {
    return !!service.discount;
  }

  public calculateDiscount(service: RestaurantMenuGetListModel): number | null {
    const price = service.price ?? 1;
    return price - ((service.discount ?? 1) / 100) * price;
  }

  public onMouseOver(id: number | undefined) {
    this.selectedServiceId = id;
  }

  public isMenuActive(id: number | undefined) {
    return this.selectedServiceId === id;
  }

  public addToCart(id: number | undefined): void {
    const model: CartPostModel = {
      quantity: 1,
      productId: id,
    };

    this.cartService.post(model).subscribe({
      next: (response: CartProductCountModel) => {
        this.snackBarService.openSuccess('Product added to the cart.', 1000);

        localStorage.setItem(
          'cartCount',
          JSON.stringify(response.numberOfProducts ?? 0)
        );
      },
      error: (error: any) => {
        if (error.error.includes('InvalidProductSelectionException')) {
          this.snackBarService.openError(
            'The cart already contains products from a restaurant. If you want to order from a different restaurant, please clear the cart first.'
          );
        }
      },
    });
  }
}
