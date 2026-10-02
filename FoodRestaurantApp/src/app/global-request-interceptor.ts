import {
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';

export class GlobalRequestInterceptor implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const restaurantId = localStorage.getItem('restaurantId') ?? '';
    const userId = localStorage.getItem('userId') ?? '';
    const cartId = localStorage.getItem('cartId') ?? '';

    const requestClone = request.clone({
      setHeaders: {
        RestaurantId: restaurantId,
        UserId: userId,
        CartId: cartId,
      },
    });

    return next.handle(requestClone);
  }
}
