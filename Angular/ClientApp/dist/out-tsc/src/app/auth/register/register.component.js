import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
let RegisterComponent = class RegisterComponent {
    constructor(formBuilder, router, authService) {
        this.formBuilder = formBuilder;
        this.router = router;
        this.authService = authService;
        this.fullName = '';
        this.email = '';
        this.password = '';
        this.isLoadingResults = false;
        this.matcher = new MyErrorStateMatcher();
    }
    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            'fullName': [null, Validators.required],
            'email': [null, Validators.required],
            'password': [null, Validators.required]
        });
    }
    onFormSubmit(form) {
        this.authService.register(form)
            .subscribe(res => {
            this.router.navigate(['login']);
        }, (err) => {
            console.log(err);
            alert(err.error);
        });
    }
};
RegisterComponent = tslib_1.__decorate([
    Component({
        selector: 'app-register',
        templateUrl: './register.component.html',
        styleUrls: ['./register.component.css']
    })
], RegisterComponent);
export { RegisterComponent };
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher {
    isErrorState(control, form) {
        const isSubmitted = form && form.submitted;
        return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
    }
}
//# sourceMappingURL=register.component.js.map