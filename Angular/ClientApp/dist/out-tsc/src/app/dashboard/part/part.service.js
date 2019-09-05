import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
let PartService = class PartService {
    constructor(http) {
        this.http = http;
        this.url = "http://aronportal.cf/";
    }
    AllUserDetails() {
        return this.http.get(this.url + 'api/Parts');
    }
    getInventory() {
        return this.http.get(this.url + 'api/InventoryCategories');
    }
    getGroup() {
        return this.http.get(this.url + 'api/ItemGroups');
    }
    getVendors() {
        return this.http.get(this.url + 'api/Vendors');
    }
    addPart(user) {
        this.http.post(this.url + 'api/parts/', user)
            .subscribe(data => {
            console.log("POst", data);
        });
    }
    editFunction(user) {
        this.http.put(this.url + 'api/Parts/' + JSON.parse(user).id, user)
            .subscribe(data => {
            console.log("PUT Request is successful ", data);
        }, error => {
            console.log("Rrror", error);
        });
    }
    deleteEmp(id) {
        this.http.delete(this.url + 'api/Parts/' + id)
            .subscribe(data => {
            alert(data);
        }, error => {
            console.log("Rrror", error);
        });
    }
};
PartService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], PartService);
export { PartService };
//# sourceMappingURL=part.service.js.map