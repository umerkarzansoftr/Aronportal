import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { of, BehaviorSubject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
let AuthService = class AuthService {
    constructor(http) {
        this.http = http;
        this.loggedIn = new BehaviorSubject(false); // {1}
        this.apiUrl = 'http://aronportal.cf/api/auth/';
    }
    login(data) {
        return this.http.post(this.apiUrl + 'login', data)
            .pipe(tap(_ => this.log('login')), catchError(this.handleError('login', [])));
    }
    isLoggedIn() {
        return localStorage.getItem('token') !== null;
    }
    register(data) {
        return this.http.post(this.apiUrl + 'register', data)
            .pipe(tap(_ => this.log('login')), catchError(this.handleError('login', [])));
    }
    handleError(operation = 'operation', result) {
        return (error) => {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead
            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);
            // Let the app keep running by returning an empty result.
            return of(result);
        };
    }
    /** Log a HeroService message with the MessageService */
    log(message) {
        console.log(message);
    }
};
AuthService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthService);
export { AuthService };
//# sourceMappingURL=auth.service.js.map