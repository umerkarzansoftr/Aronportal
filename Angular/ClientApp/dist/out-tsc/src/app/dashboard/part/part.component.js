import * as tslib_1 from "tslib";
import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { FormControl, FormGroup } from '@angular/forms';
let PartComponent = class PartComponent {
    constructor(service, changeDetectorRefs) {
        this.service = service;
        this.changeDetectorRefs = changeDetectorRefs;
        this.mainForm = new FormGroup({
            id: new FormControl(''),
            partNumber: new FormControl(''),
            partName: new FormControl(''),
            inventoryCategoryId: new FormControl(''),
            itemGroupId: new FormControl(''),
            partDescription: new FormControl(''),
            partCost: new FormControl(''),
            partQuantity: new FormControl(''),
            partVendor: new FormControl('')
        });
        this.displayedColumns = ['partNumber', 'partName', 'inventoryCategory', 'componentGroup', 'partDescription', 'partQuantity', 'partCost', 'partVendor', 'edit', 'del'];
        this.isShown = false;
        this.isEdit = false;
    }
    ngOnInit() {
        this.mainForm.controls['id'].disable();
        this.service.getInventory().subscribe(data => {
            this.testModel = data;
        });
        this.service.getGroup().subscribe(data => {
            this.Group = data;
        });
        this.service.getVendors().subscribe(data => {
            this.vendorLi = data;
        });
        this.Updatetbl();
    }
    Updatetbl() {
        this.service.AllUserDetails().subscribe(data => {
            this.dataSource = new MatTableDataSource(data);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        });
    }
    ;
    refreshCustomersList() {
        this.service.AllUserDetails()
            .subscribe(res => {
            console.log(res);
            this.dataSource = new MatTableDataSource(res);
            this.changeDetectorRefs.detectChanges();
        }, err => {
            console.log(err);
        });
    }
    toggleShow() {
        this.isShown = !this.isShown;
    }
    deleteEmp(id) {
        debugger;
        this.service.deleteEmp(id);
        this.ngOnInit();
    }
    loadEmployeeToEdit(user) {
        this.mainForm.controls['id'].enable();
        this.isShown = true;
        this.isEdit = true;
        this.mainForm.patchValue({
            id: user.id,
            partNumber: user.partNumber,
            partName: user.partName,
            inventoryCategoryId: user.inventoryCategoryId,
            itemGroupId: user.itemGroupId,
            partDescription: user.partDescription,
            partCost: user.partCost,
            partQuantity: user.partQuantity,
            partVendor: user.partVendor
        });
    }
    onSubmit() {
        if (this.isEdit) {
            this.service.editFunction(JSON.stringify(this.mainForm.value));
            this.isShown = false;
            this.mainForm.controls['id'].disable();
            this.isEdit = false;
        }
        else {
            this.service.addPart(JSON.stringify(this.mainForm.value));
            this.isShown = false;
        }
        this.ngOnInit();
    }
    applyFilter(filterValue) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
};
tslib_1.__decorate([
    ViewChild(MatPaginator, { static: true })
], PartComponent.prototype, "paginator", void 0);
tslib_1.__decorate([
    ViewChild(MatSort, { static: true })
], PartComponent.prototype, "sort", void 0);
PartComponent = tslib_1.__decorate([
    Component({
        selector: 'app-part',
        templateUrl: './part.component.html',
        styleUrls: ['./part.component.css']
    })
], PartComponent);
export { PartComponent };
//# sourceMappingURL=part.component.js.map