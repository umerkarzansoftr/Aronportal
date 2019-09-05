import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { ErrorStateMatcher } from '@angular/material/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    infoMessage = '';
  email = '';
  password = '';
  matcher = new MyErrorStateMatcher();
  isLoadingResults = false;

  constructor(private formBuilder: FormBuilder, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      'email': [null, Validators.required],
      'password': [null, Validators.required]
    });
  }
  onFormSubmit(form: NgForm) {
    this.authService.login(form)
        .subscribe(res => {
            if (res.StatusCode===401) {
                this.infoMessage = 'Invalid Email or Password';
            }
        console.log(res);
        if (res.StatusDescription) {
          localStorage.setItem('token', res.StatusDescription);
          this.authService.loggedIn.next(true);
          this.router.navigate(['dashboard/part']);
        }
      }, (err) => {
        console.log(err);
      });
  }
  register() {
    this.router.navigate(['reg']);
  }


}
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
