import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
let TokenInterceptor = class TokenInterceptor {
    constructor(router, loaderService) {
        this.router = router;
        this.loaderService = loaderService;
    }
    intercept(request, next) {
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
        return next.handle(request).pipe(map((event) => {
            if (event instanceof HttpResponse) {
                console.log('event--->>>', event);
                this.loaderService.hide();
            }
            return event;
        }), catchError((error) => {
            this.loaderService.hide();
            console.log(error);
            if (error.status === 401) {
                this.router.navigate(['login']);
            }
            if (error.status === 400) {
                alert(error.error);
            }
            return throwError(error);
        }));
    }
};
TokenInterceptor = tslib_1.__decorate([
    Injectable()
], TokenInterceptor);
export { TokenInterceptor };
//# sourceMappingURL=token.interceptor.js.map