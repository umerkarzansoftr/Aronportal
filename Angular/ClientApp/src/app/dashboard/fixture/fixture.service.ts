import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Fixture } from '../../model/Fixture';
@Injectable({
  providedIn: 'root'
})
export class FixtureService {
    url = "http://localhost:5000/";

  constructor(private http: HttpClient) { }

  getFixtures(): Observable<object[]> {
    return this.http.get<object[]>(this.url +'api/Fixtures');
  }
  getFixturesCategory(): Observable<object[]> {
    return this.http.get<object[]>(this.url + 'api/FixtureCategories');
  }
  getFixtuePart(id): Observable<object[]>{
    return this.http.get<object[]>(this.url + 'api/FixtureParts/' + id );
  }
  addFixture(dataArray, partForm) {

    this.http.post<Fixture>(this.url + 'api/Fixtures/', dataArray)
      .subscribe(
        data => {
          console.log("POst", data)
          
          partForm.forEach(item => item["fixtureId"] = data.id);
          this.addParts(partForm);
        }
      )
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

      .subscribe(

        data => {

          console.log("PUT Request is successful ", data);
         
          
        },

        error => {

          console.log("Rrror", error);

        }

      );
  }
}

