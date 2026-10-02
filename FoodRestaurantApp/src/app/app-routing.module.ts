import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RestaurantsComponent } from './components/restaurants/restaurants.component';
import { RestaurantAdministrationComponent } from './components/restaurant-administration/restaurant-administration.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { RestaurantGuard } from './guards/restaurant-guard';
import { UserAccountComponent } from './components/user-account/user-account.component';
import { RestaurantDetailComponent } from './components/restaurants/restaurant-detail/restaurant-detail.component';
import { CartComponent } from './components/cart/cart.component';

const routes: Routes = [
  {
    path: 'restaurants',
    component: RestaurantsComponent,
  },
  {
    path: 'restaurant-administration',
    component: RestaurantAdministrationComponent,
    canActivate: [RestaurantGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'user-account',
    component: UserAccountComponent,
  },
  {
    path: 'restaurant/:restaurantId',
    component: RestaurantDetailComponent,
  },
  {
    path: 'cart',
    component: CartComponent,
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
