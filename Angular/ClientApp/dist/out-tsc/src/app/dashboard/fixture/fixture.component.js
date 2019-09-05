import * as tslib_1 from "tslib";
import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { FormControl, FormGroup } from '@angular/forms';
import { fixtureForm } from '../../model/fixtureForm';
let FixtureComponent = class FixtureComponent {
    constructor(service, config, partService) {
        this.service = service;
        this.config = config;
        this.partService = partService;
        this.fixtureForm = new fixtureForm();
        this.dataArray = [];
        this.second = [];
        this.displayedColumns = ['fixtureName', 'fixtureCode', 'fixtureCategory', 'fixtureCost', 'copy', 'edit', 'del'];
        this.mainForm = new FormGroup({
            id: new FormControl(''),
            fixtureName: new FormControl(''),
            fixtureCode: new FormControl(''),
            fixtureCategoryId: new FormControl(''),
            fixtureCost: new FormControl(''),
        });
        this.sortedData = this.dataArray.slice();
    }
    loadEmployeeToEdit(user) {
        this.service.getFixtuePart(user.id).subscribe(data => {
            this.dataArray = data;
            for (let item of this.dataArray) {
                this.sortedData = this.dataArray.slice();
            }
        });
        this.mainForm.controls['id'].enable();
        this.isShown = true;
        this.isEdit = true;
        this.mainForm.patchValue({
            id: user.id,
            fixtureName: user.fixtureName,
            fixtureCode: user.fixtureCode,
            fixtureCategoryId: user.fixtureCategoryId,
            fixtureCost: user.fixtureCost
        });
    }
    sortData(sort) {
        const data = this.dataArray.slice();
        if (!sort.active || sort.direction === '') {
            this.sortedData = data;
            return;
        }
        this.sortedData = data.sort((a, b) => {
            const isAsc = sort.direction === 'asc';
            switch (sort.active) {
                case 'name': return compare(a.name, b.name, isAsc);
                case 'calories': return compare(a.calories, b.calories, isAsc);
                case 'fat': return compare(a.fat, b.fat, isAsc);
                case 'carbs': return compare(a.carbs, b.carbs, isAsc);
                default: return 0;
            }
        });
    }
    ngOnInit() {
        this.mainForm.controls['id'].disable();
        this.getAllFixtures();
        this.isEdit = false;
        this.getAllParts();
        this.isShown = false;
        this.service.getFixturesCategory().subscribe(data => {
            this.testModel = data;
        });
    }
    addForm() {
        this.sortedData = this.dataArray.slice();
        this.fixtureForm = new fixtureForm();
        this.fixtureForm.quantity = 1;
        this.dataArray.push(this.fixtureForm);
        this.sortedData = this.dataArray.slice();
    }
    getAllFixtures() {
        this.service.getFixtures().subscribe(data => {
            this.dataSource = new MatTableDataSource(data);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        });
    }
    getAllParts() {
        this.partService.AllUserDetails().subscribe(data => {
            this.anyD = data;
        });
    }
    getNamebYiD(ID) {
        //console.log(ID);
        //console.log(this.anyD.find(f => f.id === ID));
        //console.log(this.anyD.find(item => item.id === ID));
        if (ID === undefined)
            return;
        let temp;
        temp = this.anyD.find(item => item.id === ID);
        if (temp === undefined)
            return;
        return (temp.partName);
    }
    onAdd($event, i) {
        this.dataArray[i].cost = this.anyD.find(item => item.id == this.dataArray[i].partId).partCost;
        this.mainForm.patchValue({
            fixtureCost: (this.dataArray.map(bill => bill.cost * bill.quantity).reduce((acc, bill) => bill + acc))
        });
    }
    onKey(event) {
        this.mainForm.patchValue({
            fixtureCost: (this.dataArray.map(bill => bill.cost * bill.quantity).reduce((acc, bill) => bill + acc))
        });
    }
    toggleShow() {
        this.isShown = !this.isShown;
    }
    onSubmit() {
        if (!this.isEdit) {
            this.service.addFixture(this.mainForm.value, this.dataArray);
        }
        else {
            this.service.editFunction(this.mainForm.value, this.dataArray);
            this.isShown = false;
            this.mainForm.controls['id'].disable();
            this.isEdit = false;
        }
    }
    applyFilter(filterValue) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
    removeForm(index) {
        this.sortedData = this.dataArray.slice();
        this.dataArray.splice(index, 1);
        this.sortedData = this.dataArray.slice();
        if (index > 1) {
            this.mainForm.patchValue({
                fixtureCost: (this.dataArray.map(bill => bill.cost).reduce((acc, bill) => bill + acc))
            });
        }
        else {
            this.mainForm.patchValue({
                fixtureCost: 0
            });
        }
    }
};
tslib_1.__decorate([
    ViewChild(MatPaginator, { static: true })
], FixtureComponent.prototype, "paginator", void 0);
tslib_1.__decorate([
    ViewChild(MatSort, { static: true })
], FixtureComponent.prototype, "sort", void 0);
FixtureComponent = tslib_1.__decorate([
    Component({
        selector: 'app-fixture',
        templateUrl: './fixture.component.html',
        styleUrls: ['./fixture.component.css']
    })
], FixtureComponent);
export { FixtureComponent };
function compare(a, b, isAsc) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
//# sourceMappingURL=fixture.component.js.map