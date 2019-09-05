import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
let LoginComponent = class LoginComponent {
    constructor(formBuilder, router, authService) {
        this.formBuilder = formBuilder;
        this.router = router;
        this.authService = authService;
        this.infoMessage = '';
        this.email = '';
        this.password = '';
        this.matcher = new MyErrorStateMatcher();
        this.isLoadingResults = false;
    }
    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            'email': [null, Validators.required],
            'password': [null, Validators.required]
        });
    }
    onFormSubmit(form) {
        this.authService.login(form)
            .subscribe(res => {
            if (res.StatusCode === 401) {
                this.infoMessage = 'Invalid Email or Password';
            }
            console.log(res);
            if (res.StatusDescription) {
                localStorage.setItem('token', res.StatusDescription);
                this.authService.loggedIn.next(true);
                this.router.navigate(['dashboard/part']);
            }
        }, (err) => {
            console.log(err);
        });
    }
    register() {
        this.router.navigate(['reg']);
    }
};
LoginComponent = tslib_1.__decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    })
], LoginComponent);
export { LoginComponent };
export class MyErrorStateMatcher {
    isErrorState(control, form) {
        const isSubmitted = form && form.submitted;
        return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
    }
}
//# sourceMappingURL=login.component.js.map