import { Component, OnInit, ViewChild } from '@angular/core';
import { FixtureList } from '../../model/fixtureList';
import { partList } from '../../model/partList';
import { PurchaseorderService } from '../purchaseorder/purchaseorder.service'
import { Sort, MatSort } from '@angular/material/sort';
import { FormGroup, FormControl } from '@angular/forms';
import { MatTableDataSource, MatPaginator } from '@angular/material';
@Component({
  selector: 'app-purchaseorder',
  templateUrl: './purchaseorder.component.html',
  styleUrls: ['./purchaseorder.component.css']
})
export class PurchaseorderComponent implements OnInit {
  FixtureList = new FixtureList();
  partList = new partList();
  fixturesArray = [];
  partsArray = [];
  isShown: boolean;
  allParts: any[];
  allPOS: any[];
  allFixtures: any[];
  allVendors: any[];
  sortedFixtures: any[];
  sortedParts: any[];
  dataSource: MatTableDataSource<object[]>;
  displayedColumns: string[] = ['ponumber', 'poname', 'customerId', 'porecDate', 'poestShipDate', 'poprice', 'pocost','pocompleted', 'edit', 'del'];
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  mainForm = new FormGroup({
    id: new FormControl(''),
    ponumber: new FormControl(''),
    porecDate: new FormControl(''),
    poestShipDate: new FormControl(''),
    poactShipDate: new FormControl(''),
    pocost: new FormControl(''),
    poprice: new FormControl(''),
    pocompleted: new FormControl(''),
    createdAt: new FormControl(''),
    customerId: new FormControl(''),
    poname: new FormControl('')
    
  });
    isEdit: boolean;


  constructor(private service: PurchaseorderService) {
    this.sortedFixtures = this.fixturesArray.slice();
    this.sortedParts = this.partsArray.slice();
  }
  sortFixture(sort: Sort) {
    const data = this.fixturesArray.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedFixtures = data;
      return;
    }

    this.sortedFixtures = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'calories': return compare(a.calories, b.calories, isAsc);
        case 'fat': return compare(a.fat, b.fat, isAsc);
        case 'carbs': return compare(a.carbs, b.carbs, isAsc);
        case 'protein': return compare(a.protein, b.protein, isAsc);
        default: return 0;
      }
    });
  }
  sortParts(sort: Sort) {
    const data = this.partsArray.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedParts = data;
      return;
    }

    this.sortedParts = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'part': return compare(a.name, b.name, isAsc);
        case 'quantity': return compare(a.calories, b.calories, isAsc);
       
        
        default: return 0;
      }
    });
  }
  ngOnInit() {
    this.isShown = false;
    this.mainForm.controls['id'].disable();
    this.getVendors();
    this.getPOS();
    this.getFixtures();
    this.getParts();
   
  }
  toggleShow() {
    this.isShown = !this.isShown;
  }
  getFixtures() {
    this.service.getFixtures().subscribe(data => {
      this.allFixtures = data;
    });
  }
  getVendors() {
    this.service.getVendors().subscribe(data => {
      this.allVendors = data;
    });
  }
  getParts() {
    this.service.getParts().subscribe(data => {
      this.allParts = data;
    })
  }
  getPOS() {
    this.service.getPOS().subscribe(data => {
      this.dataSource = new MatTableDataSource(<any>data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }
  addFixture() {
    this.FixtureList = new FixtureList();
    this.fixturesArray.push(this.FixtureList);
    this.FixtureList.fixtureQuantity = 1;
    this.FixtureList.fixtureCommision = 1;
    this.sortedFixtures = this.fixturesArray.slice();
  }
  addPart() {
    this.partList = new partList();
    this.partsArray.push(this.partList);
    this.partList.quantity = 1;

    this.sortedParts = this.partsArray.slice();
  }
  removeForm(index) {
    this.sortedFixtures = this.fixturesArray.slice();
    this.fixturesArray.splice(index, 1);
    this.sortedFixtures = this.fixturesArray.slice();
  }
  removeParts(index) {
    this.sortedParts = this.partsArray.slice();
    this.partsArray.splice(index, 1);
    this.sortedParts = this.partsArray.slice();
  }
  loadEmployeeToEdit(user) {
    this.service.getPOFixture(user.id).subscribe(data => {
      this.fixturesArray = data;
      for (let item of this.fixturesArray) {

        this.sortedFixtures = this.fixturesArray.slice();


      }
    });
    this.service.getPartsPO(user.id).subscribe(data => {
      this.partsArray = data;
      for (let item of this.partsArray) {

        this.sortedParts = this.partsArray.slice();


      }
    });

    this.mainForm.controls['id'].enable();
    this.isShown = true;
    this.isEdit = true;
    this.mainForm.patchValue({
      id: user.id,
     ponumber:user.ponumber,
      porecDate: user.porecDate,
      poestShipDate: user.poestShipDate,
      poactShipDate: user.poactShipDate,
      pocost: user.pocost,
      poprice: user.poprice,
      pocompleted: user.pocompleted,
      createdAt: user.porecDate,
      customerId: user.customerId,
      poname: user.poname

    });

  }
  onSubmit() {
    if (!this.isEdit) {
      this.service.addPo(this.mainForm.value, this.fixturesArray, this.partsArray);
    } else {
      this.service.editFunction(this.mainForm.value, this.partsArray, this.fixturesArray);
      this.isShown = false;
      this.mainForm.controls['id'].disable();
      this.isEdit = false;
    }
    
  }
  onAdd($event, i) {
    this.fixturesArray[i].fixtureCost = this.allFixtures.find(item => item.id == this.fixturesArray[i].fixtureId).fixtureCost;
    this.fixturesArray[i].fixturePrice = this.allFixtures.find(item => item.id == this.fixturesArray[i].fixtureId).fixtureCost;
    //this.mainForm.patchValue({
    //  fixtureCost: (this.dataArray.map(bill => bill.cost * bill.quantity).reduce((acc, bill) => bill + acc))
    //});
  }
}
function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
