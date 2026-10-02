import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { UserRegisterModel } from '../models/users/user-register.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserLoginGetModel } from '../models/users/user-login-get.model';
import { Router } from '@angular/router';
import { UserLoginModel } from '../models/users/user-login.model';
import { UserSecurityChangePasswordModel } from '../models/user-security/user-security-change-password.model';
import { UserAccountDetailsGetModel } from '../models/users/user-account-details-get.model';
import { SnackbarService } from './snackbar.service';
import { UserCardGetList } from '../models/users/user-card-get-list.model';
import { UserAddressPostPatchModel } from '../models/users/user-address-post-patch.model';
import { UserAddressGetListModel } from '../models/users/user-address-get-list.model';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  private userSubject = new BehaviorSubject<UserLoginGetModel | null>(null);
  user$ = this.userSubject.asObservable();
  private userKey: string = 'user';

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private snackbarService: SnackbarService
  ) {
    super();

    const existingUser = localStorage.getItem(this.userKey);
    if (existingUser != null) {
      this.userSubject.next(JSON.parse(existingUser));
    }

    this.user$ = this.userSubject.asObservable();
  }

  public register(data: UserRegisterModel): Observable<any> {
    return this.httpClient.post<UserLoginGetModel>(
      `${this.baseUrl}users/register`,
      data
    );
  }

  public registerSucceeded(response: UserLoginGetModel): void {
    localStorage.setItem(
      'restaurantId',
      JSON.stringify(response.restaurantId ?? '')
    );
    localStorage.setItem('userId', JSON.stringify(response.userId ?? ''));
    localStorage.setItem('cartId', JSON.stringify(response.cartId ?? ''));
    localStorage.setItem(
      'cartCount',
      JSON.stringify(response.numberOfProducts ?? '')
    );

    console.log(response);
    this.userSubject.next(response);
    console.log(this.userSubject.value);

    this.navigate(response.restaurantId);
  }

  public registerFailed(error: any): void {
    const errorMessage = error?.error as string;

    if (!errorMessage) {
      return;
    }

    if (errorMessage.includes('UserEmailAlreadyExistsException')) {
      this.snackbarService.openError(
        'This email is registered with another account.',
        2000
      );
    }

    if (errorMessage.includes('UserRegistrationPasswordDoNotMatchException')) {
      this.snackbarService.openError('Confirmation password is invalid.', 2000);
    }
  }

  public login(data: UserLoginModel): Observable<any> {
    return this.httpClient.post<UserLoginGetModel>(
      `${this.baseUrl}users/login`,
      data
    );
  }

  public loginFailed(error: any): void {
    const errorMessage = error?.error as string;

    if (!errorMessage) {
      return;
    }

    if (errorMessage.includes('UserInvalidLoginCredentialsException')) {
      this.snackbarService.openError('Invalid credentials.', 2000);
    }
  }

  public loginSucceeded(response: UserLoginGetModel): void {
    localStorage.setItem(this.userKey, JSON.stringify(response));

    localStorage.setItem(
      'restaurantId',
      JSON.stringify(response.restaurantId ?? '')
    );
    localStorage.setItem('userId', JSON.stringify(response.userId ?? ''));
    localStorage.setItem('cartId', JSON.stringify(response.cartId ?? ''));
    localStorage.setItem(
      'cartCount',
      JSON.stringify(response.numberOfProducts ?? '')
    );

    this.userSubject.next(response);

    this.navigate(response.restaurantId);
  }

  public changePassword(
    data: UserSecurityChangePasswordModel
  ): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}users/change-password`, data);
  }

  public getAccountDetails(
    email: string
  ): Observable<UserAccountDetailsGetModel> {
    return this.httpClient.get<UserAccountDetailsGetModel>(
      `${this.baseUrl}users/account-details?email=${email}`
    );
  }

  public getUserCards(): Observable<UserCardGetList[]> {
    return this.httpClient.get<UserCardGetList[]>(`${this.baseUrl}users/cards`);
  }

  public deleteCard(cardId: number): Observable<any> {
    return this.httpClient.post(
      `${this.baseUrl}users/card/delete/${cardId}`,
      {}
    );
  }

  public changeAccountDetails(
    data: UserAccountDetailsGetModel
  ): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}users/account-details`, data);
  }

  public addOrUpdateCard(data: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}users/add-update-card`, data);
  }

  public deleteAddress(addressId: number): Observable<any> {
    return this.httpClient.post(
      `${this.baseUrl}users/address/delete/${addressId}`,
      {}
    );
  }

  public addOrUpdateAddress(data: UserAddressPostPatchModel): Observable<any> {
    return this.httpClient.post(
      `${this.baseUrl}users/add-update-address`,
      data
    );
  }

  public getAddresses(): Observable<UserAddressGetListModel[]> {
    return this.httpClient.get<UserAddressGetListModel[]>(
      `${this.baseUrl}users/addresses`
    );
  }

  public logout(): void {
    localStorage.removeItem(this.userKey);
    localStorage.removeItem('restaurant-image');
    localStorage.removeItem('restaurant-name');

    localStorage.removeItem('restaurantId');
    localStorage.removeItem('userId');
    localStorage.removeItem('cartId');
    localStorage.removeItem('cartCount');

    this.userSubject.next(null);
    this.router.navigateByUrl('login');
  }

  public isLoggedIn(): boolean {
    return this.userSubject.value != null;
  }

  public getUser(): UserLoginGetModel | null {
    const existingUser = localStorage.getItem(this.userKey);
    return existingUser != null ? JSON.parse(existingUser) : null;
  }

  private navigate(restaurantId: number | null): void {
    const route = !!restaurantId ? 'restaurant-administration' : 'restaurants';
    this.router.navigateByUrl(route);
  }
}
