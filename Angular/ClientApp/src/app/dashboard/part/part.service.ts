import { Injectable } from '@angular/core';
import { IParts } from '../../model/iparts';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PartService {
    url = "http://localhost:5000/";
  constructor(private http: HttpClient) { }
  AllUserDetails(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'api/Parts');
  }
  getInventory(): Observable<object> {
    
    return this.http.get<object>(this.url + 'api/InventoryCategories');
  }
  
  getGroup(): Observable<object> {

    return this.http.get<object>(this.url + 'api/ItemGroups');
  }
  getVendors(): Observable<object> {

    return this.http.get<object>(this.url + 'api/Vendors');
  }
  addPart(user) {
    this.http.post(this.url + 'api/parts/', user)
      .subscribe(
        data => {
          console.log("POst", data)
        }
      )
  }
  editFunction(user) {
   
    this.http.put(this.url + 'api/Parts/' + JSON.parse(user).Id, user)

      .subscribe(

        data => {

          console.log("PUT Request is successful ", data);

        },

        error => {

          console.log("Rrror", error);

        }

      );
  }
  deleteEmp(id) {
      
    this.http.delete(this.url + 'api/Parts/' +id)

      .subscribe(

        data => {

          console.log(data);

        },

        error => {

            alert(error);

        }

      );
  }
}  
