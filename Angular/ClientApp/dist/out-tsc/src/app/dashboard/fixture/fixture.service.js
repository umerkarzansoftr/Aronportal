import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
let FixtureService = class FixtureService {
    constructor(http) {
        this.http = http;
        this.url = "http://aronportal.cf/";
    }
    getFixtures() {
        return this.http.get(this.url + 'api/Fixtures');
    }
    getFixturesCategory() {
        return this.http.get(this.url + 'api/FixtureCategories');
    }
    getFixtuePart(id) {
        return this.http.get(this.url + 'api/FixtureParts/' + id);
    }
    addFixture(dataArray, partForm) {
        this.http.post(this.url + 'api/Fixtures/', dataArray)
            .subscribe(data => {
            console.log("POst", data);
            partForm.forEach(item => item["fixtureId"] = data.id);
            this.addParts(partForm);
        });
    }
    addParts(form) {
        for (var key in form) {
            this.http.post(this.url + 'api/FixtureParts', form[key]).subscribe();
        }
    }
    editFunction(user, partForm) {
        //user.forEach(item => item['partLines'] = partForm);
        user['FixtureParts'] = partForm;
        console.log(user);
        this.http.put(this.url + 'api/Fixtures/' + user.id, user)
            .subscribe(data => {
            console.log("PUT Request is successful ", data);
        }, error => {
            console.log("Rrror", error);
        });
    }
};
FixtureService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], FixtureService);
export { FixtureService };
//# sourceMappingURL=fixture.service.js.map