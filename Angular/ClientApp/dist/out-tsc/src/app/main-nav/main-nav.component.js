import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Breakpoints } from '@angular/cdk/layout';
import { map, share } from 'rxjs/operators';
let MainNavComponent = class MainNavComponent {
    constructor(breakpointObserver, router) {
        this.breakpointObserver = breakpointObserver;
        this.router = router;
        this.isHandset$ = this.breakpointObserver.observe(Breakpoints.Handset)
            .pipe(map(result => result.matches), share());
    }
};
MainNavComponent = tslib_1.__decorate([
    Component({
        selector: 'app-main-nav',
        templateUrl: './main-nav.component.html',
        styleUrls: ['./main-nav.component.css']
    })
], MainNavComponent);
export { MainNavComponent };
//# sourceMappingURL=main-nav.component.js.map