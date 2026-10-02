import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CartGetModel } from 'src/app/models/carts/cart-get.model';
import { CartPostModel } from 'src/app/models/carts/cart-post.model';
import { CartProductGetModel } from 'src/app/models/carts/cart-product-get-model';
import { UserAddressGetListModel } from 'src/app/models/users/user-address-get-list.model';
import { UserCardGetList } from 'src/app/models/users/user-card-get-list.model';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  public cart: CartGetModel = new CartGetModel();
  public addresses: UserAddressGetListModel[] = [];
  public cards: UserCardGetList[] = [];

  public isLoading: boolean = false;

  public selectedAddressId: number = -1;
  public selectedCardId: number = -1;

  constructor(
    private cartService: CartService,
    private userService: UserService,
    private orderServie: OrderService,
    private router: Router,
    private snackbarService: SnackbarService
  ) {}

  ngOnInit(): void {
    this.isLoading = true;

    const cart$ = this.cartService.get();
    const cards$ = this.userService.getUserCards();
    const addresses$ = this.userService.getAddresses();

    forkJoin({ cards: cards$, cart: cart$, addresses: addresses$ }).subscribe(
      (response: any) => {
        this.cart = response.cart;
        this.cards = response.cards;
        this.addresses = response.addresses;

        this.isLoading = false;
      }
    );
  }

  public calculatePrice(product: CartProductGetModel): number {
    return product.price * product.quantity;
  }

  public calculatePriceWithDiscount(product: CartProductGetModel): number {
    const totalPrice = this.calculatePrice(product);

    return totalPrice - ((product.discount ?? 0) / 100) * totalPrice;
  }

  public updateQuantity(
    product: CartProductGetModel,
    quantityIncrement: number
  ): void {
    product.quantity += quantityIncrement;

    if (product.quantity === 0) {
      this.cart.products.splice(this.cart.products.indexOf(product), 1);

      this.cartService.delete(product.cartProductId).subscribe((_: any) => {});
    } else {
      const data: CartPostModel = {
        productId: product.productId,
        quantity: quantityIncrement,
      };

      this.cartService.post(data).subscribe((_: any) => {});
    }
  }

  public calculateProductsTotalPrice(): number {
    let totalPrice = 0;
    const products = this.cart.products;

    for (let i = 0; i < products.length; i++) {
      totalPrice += !!products[i].discount
        ? this.calculatePriceWithDiscount(products[i])
        : this.calculatePrice(products[i]);
    }

    return totalPrice;
  }

  public getTransport(): number {
    return this.cart.transport ?? 0;
  }

  public calculateTotalPrice(): number {
    return this.calculateProductsTotalPrice() + this.getTransport();
  }

  public placeOrder(): void {
    if (this.selectedAddressId === -1) {
      this.snackbarService.openError('Please select a delivery address.', 2000);
      return;
    }

    if (this.selectedCardId === -1) {
      this.snackbarService.openError('Please select a payment card.', 2000);
      return;
    }

    this.orderServie
      .placeOrder(this.selectedAddressId, this.selectedCardId)
      .subscribe({
        next: (_: any) => {
          localStorage.removeItem('cartCount');
          localStorage.setItem('userAccountMenu', 'Orders');
          this.router.navigate(['user-account']);
        },
        error: (error: any) => {
          if (error.error.includes('RestaurantClosedException')) {
            this.snackbarService.openError(
              'Restaurant is closed, please come back later.',
              3000
            );
          }
        },
      });
  }
}
