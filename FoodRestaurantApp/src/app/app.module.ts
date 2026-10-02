import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatSelectModule } from '@angular/material/select';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RestaurantsComponent } from './components/restaurants/restaurants.component';
import { RestaurantAdministrationComponent } from './components/restaurant-administration/restaurant-administration.component';
import { RestaurantProfileComponent } from './components/restaurant-profile/restaurant-profile.component';
import { RestaurantMenuComponent } from './components/restaurant-menu/restaurant-menu.component';
import { MatDialogModule } from '@angular/material/dialog';
import { RestaurantMenuAddComponent } from './components/restaurant-menu/restaurant-menu-add/restaurant-menu-add.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DialogConfirmationComponent } from './components/dialog-confirmation/dialog-confirmation.component';
import { ViewManagementComponent } from './components/view-management/view-management.component';
import { CategoriesComponent } from './components/restaurant-menu/categories/categories.component';
import { ProductsComponent } from './components/restaurant-menu/products/products.component';
import { CategoryAddEditComponent } from './components/restaurant-menu/category-add-edit/category-add-edit.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { CustomButtonComponent } from './components/custom-button/custom-button.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { ScheduleAddEditComponent } from './components/schedule/schedule-add-edit/schedule-add-edit.component';
import { ProfileComponent } from './components/restaurant-profile/profile/profile.component';
import { ReviewsComponent } from './components/restaurant-profile/reviews/reviews.component';
import { ReviewDetailsComponent } from './components/restaurant-profile/review-details/review-details.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { RestaurantDashboardComponent } from './components/restaurant-dashboard/restaurant-dashboard.component';
import { ChartModule } from 'angular-highcharts';
import { RestaurantDashboardCardComponent } from './components/restaurant-dashboard/restaurant-dashboard-card/restaurant-dashboard-card.component';
import { UserAccountComponent } from './components/user-account/user-account.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { UserSecurityComponent } from './components/user-security/user-security.component';
import { UserAccountDetailsComponent } from './components/user-account-details/user-account-details.component';
import { UserAccountDetailsPopupComponent } from './components/user-account-details/user-account-details-popup/user-account-details-popup.component';
import { UserCardsComponent } from './components/user-cards/user-cards.component';
import { UserCardAddComponent } from './components/user-cards/user-card-add/user-card-add.component';
import { GlobalRequestInterceptor } from './global-request-interceptor';
import { ProductUploadImageComponent } from './components/restaurant-menu/product-upload-image/product-upload-image.component';
import { RestaurantDetailComponent } from './components/restaurants/restaurant-detail/restaurant-detail.component';
import { RestaurantHeaderComponent } from './components/restaurants/restaurant-header/restaurant-header.component';
import { CartComponent } from './components/cart/cart.component';
import { UserAddressesComponent } from './components/user-addresses/user-addresses.component';
import { UserAddressAddComponent } from './components/user-addresses/user-address-add/user-address-add.component';
import { UserOrdersComponent } from './components/user-orders/user-orders.component';
import { RestaurantOrdersComponent } from './components/restaurant-orders/restaurant-orders.component';
import { ReviewAddComponent } from './components/restaurant-profile/review-add/review-add.component';
import { RestaurantOrderInfoComponent } from './components/restaurant-orders/restaurant-order-info/restaurant-order-info.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RestaurantsComponent,
    RestaurantAdministrationComponent,
    RestaurantProfileComponent,
    RestaurantMenuComponent,
    RestaurantMenuAddComponent,
    DialogConfirmationComponent,
    ViewManagementComponent,
    CategoriesComponent,
    ProductsComponent,
    CategoryAddEditComponent,
    CustomButtonComponent,
    ScheduleComponent,
    ScheduleAddEditComponent,
    ProfileComponent,
    ReviewsComponent,
    ReviewDetailsComponent,
    LoginComponent,
    RegisterComponent,
    RestaurantDashboardComponent,
    RestaurantDashboardCardComponent,
    UserAccountComponent,
    UserSecurityComponent,
    UserAccountDetailsComponent,
    UserAccountDetailsPopupComponent,
    UserCardsComponent,
    UserCardAddComponent,
    ProductUploadImageComponent,
    RestaurantDetailComponent,
    RestaurantHeaderComponent,
    CartComponent,
    UserAddressesComponent,
    UserAddressAddComponent,
    UserOrdersComponent,
    RestaurantOrdersComponent,
    ReviewAddComponent,
    RestaurantOrderInfoComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatInputModule,
    MatSlideToggleModule,
    MatDialogModule,
    MatIconModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatSelectModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    ChartModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GlobalRequestInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
