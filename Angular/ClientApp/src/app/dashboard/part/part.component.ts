import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { IParts } from '../../model/iparts';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { PartService } from '../part/part.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';


@Component({
  selector: 'app-part',
  templateUrl: './part.component.html',
  styleUrls: ['./part.component.css']
})
export class PartComponent implements OnInit {
  mainForm = new FormGroup({
    Id: new FormControl(''),
    partNumber: new FormControl(''),
    partName: new FormControl(''),
    inventoryCategoryId: new FormControl(''),
      ItemGroupsId: new FormControl(''),
    partDescription: new FormControl(''),
      partCost: new FormControl(''),
    partQuantity: new FormControl(''),
    partVendor: new FormControl('')
   
  });
  testModel: object;

  Group: object;
  vendorLi: object;
  allUser: IParts[];
  dataSource: MatTableDataSource<IParts>; 
  displayedColumns: string[] = ['partNumber', 'partName', 'inventoryCategory', 'componentGroup', 'partDescription', 'partQuantity', 'partCost','partVendor', 'edit', 'del' ];
  @ViewChild(MatPaginator, { static: true  }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  formText: string;
  constructor(private service: PartService, private changeDetectorRefs: ChangeDetectorRef ) {
   
  }
  ngOnInit() {
    this.mainForm.controls['Id'].disable();
    
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
       this.dataSource = new MatTableDataSource(<any>data);
       this.dataSource.paginator = this.paginator;
       this.dataSource.sort = this.sort;

     }
      
         );
  };
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
    
      this.service.deleteEmp(id);
      this.Updatetbl();
    
  }
  loadEmployeeToEdit(user) {
    this.mainForm.controls['id'].enable();
    this.isShown = true;
    this.isEdit = true;
    this.mainForm.patchValue({
      id:user.id,
      partNumber:user.partNumber,
      partName: user.partName,
      inventoryCategoryId: user.inventoryCategoryId,
      itemGroupId:user.itemGroupId,
      partDescription: user.partDescription,
      partCost: user.partCost,
      partQuantity: user.partQuantity,
      partVendor: user.partVendor
    });
    
  }
  onSubmit() {
    if (this.isEdit) {
      this.service.editFunction(JSON.stringify(this.mainForm.value))

      this.isShown = false;
      this.mainForm.controls['id'].disable();
      this.isEdit = false;
    }
    else {
      this.service.addPart(JSON.stringify(this.mainForm.value));
      this.isShown = false;
      }
      this.Updatetbl();
  }
  isShown: boolean = false;
  isEdit: boolean = false;
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}  
