import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Fixture } from '../../model/Fixture';
@Injectable({
  providedIn: 'root'
})
export class PurchaseorderService {
    url = "http://localhost:5000/";
  constructor(private http: HttpClient) { }

  getFixtures(): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/Fixtures');
  }
  getParts(): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/Parts');
  }
  getVendors(): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/Vendors');
  }
  addParts(form) {
    for (var key in form) {
      this.http.post(this.url + 'api/poparts', form[key]).subscribe();
    }
  }
  addFixtures(data) {
    this.http.post(this.url + 'api/pofixtures/', data)
      .subscribe(
        data => {
          console.log("POst", data)

          

        }
      )
  }
  getPOFixture(id): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/Pofixtures/' + id);
  }
  getPartsPO(id): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/Poparts/' + id);
  }
  addPo(mainForm, fixtures, parts) {
    
    mainForm['Poparts'] = parts;
    mainForm['Pofixtures'] = fixtures;
    this.http.post<Fixture>(this.url + 'api/purchaseorders/', mainForm)
      .subscribe(
        data => {
          console.log("POst", data)        
        }
      )
    
   
  }
  deleteEmp(user) {
    this.http.delete(this.url + 'api/purchaseorders' + user.id).subscribe();
  }
    editFunction(user, parts,fixtures) {
        //user.forEach(item => item['partLines'] = partForm);
        user['Poparts'] = parts;
        user['Pofixtures'] = fixtures;
        console.log(user);
        this.http.put(this.url + 'api/purchaseorders/'+ user.id, user)

            .subscribe(

                data => {

                    console.log("PUT Request is successful ", data);


                },

                error => {

                    console.log("Rrror", error);

                }

            );
    }
  getPOS(): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/purchaseorders');
  }
}
