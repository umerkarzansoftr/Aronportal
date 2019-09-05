import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
let PurchaseorderService = class PurchaseorderService {
    constructor(http) {
        this.http = http;
        this.url = "http://aronportal.cf/";
    }
    getFixtures() {
        return this.http.get(this.url + 'api/Fixtures');
    }
    getParts() {
        return this.http.get(this.url + 'api/Parts');
    }
    getVendors() {
        return this.http.get(this.url + 'api/Vendors');
    }
    addParts(form) {
        for (var key in form) {
            this.http.post(this.url + 'api/poparts', form[key]).subscribe();
        }
    }
    addFixtures(data) {
        this.http.post(this.url + 'api/pofixtures/', data)
            .subscribe(data => {
            console.log("POst", data);
        });
    }
    getPOFixture(id) {
        return this.http.get(this.url + 'api/Pofixtures/' + id);
    }
    getPartsPO(id) {
        return this.http.get(this.url + 'api/Poparts/' + id);
    }
    addPo(mainForm, fixtures, parts) {
        mainForm['Poparts'] = parts;
        mainForm['Pofixtures'] = fixtures;
        this.http.post(this.url + 'api/purchaseorders/', mainForm)
            .subscribe(data => {
            console.log("POst", data);
        });
    }
    deleteEmp(user) {
        this.http.delete(this.url + 'api/purchaseorders' + user.id).subscribe();
    }
    editFunction(user, parts, fixtures) {
        //user.forEach(item => item['partLines'] = partForm);
        user['Poparts'] = parts;
        user['Pofixtures'] = fixtures;
        console.log(user);
        this.http.put(this.url + 'api/purchaseorders/' + user.id, user)
            .subscribe(data => {
            console.log("PUT Request is successful ", data);
        }, error => {
            console.log("Rrror", error);
        });
    }
    getPOS() {
        return this.http.get(this.url + 'api/purchaseorders');
    }
};
PurchaseorderService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], PurchaseorderService);
export { PurchaseorderService };
//# sourceMappingURL=purchaseorder.service.js.map