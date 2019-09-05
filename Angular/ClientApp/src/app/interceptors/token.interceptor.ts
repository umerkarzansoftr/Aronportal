import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LoaderService } from '../components/shared/loader/loader.service';
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private router: Router, public loaderService: LoaderService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      this.loaderService.show();
    const token = localStorage.getItem('token');
    if (token) {
      request = request.clone({
        setHeaders: {
          'Authorization': 'Bearer ' + token
        }
      });
    }
    if (!request.headers.has('Content-Type')) {
      request = request.clone({
        setHeaders: {
          'content-type': 'application/json'
        }
      });
    }
    request = request.clone({
      headers: request.headers.set('Accept', 'application/json')
    });
      return next.handle(request).pipe(

        map((event: HttpEvent<any>) => {

        if (event instanceof HttpResponse) {
            console.log('event--->>>', event);
            this.loaderService.hide();
        }
        return event;
      }),
          catchError((error: HttpErrorResponse) => {
              this.loaderService.hide();
        console.log(error);
        if (error.status === 401) {
          this.router.navigate(['login']);
        }
        if (error.status === 400) {
          alert(error.error);
        }
        return throwError(error);
      })

      );
  }

}
