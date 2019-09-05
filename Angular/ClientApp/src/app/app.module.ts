import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoaderService } from '../app/components/shared/loader/loader.service';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { NgSelectModule } from '@ng-select/ng-select';
import {
  MatInputModule,
  MatPaginatorModule,
  MatProgressSpinnerModule,
  MatSortModule,
  MatTableModule,
  MatIconModule,
  MatSidenavModule,
  MatButtonModule,
  MatCardModule,
  MatFormFieldModule,
  MatAutocompleteModule
} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthHeaderComponent } from './auth/auth-header/auth-header.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PartComponent } from './dashboard/part/part.component';
import { MainNavComponent } from './main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { FixtureComponent } from './dashboard/fixture/fixture.component';
import { PurchaseorderComponent } from './dashboard/purchaseorder/purchaseorder.component';
import { LoaderComponent } from './components/shared/loader/loader.component'

@NgModule({
  exports: [
    NgSelectModule],
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    AuthHeaderComponent,
    DashboardComponent,
    PartComponent,
    MainNavComponent,
    FixtureComponent,
    PurchaseorderComponent,
    LoaderComponent
   
  ],
  imports: [
   BrowserModule,
    FormsModule,
    MatSidenavModule,
  HttpClientModule,
  AppRoutingModule,
  ReactiveFormsModule,
  BrowserAnimationsModule,
  MatInputModule,
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatProgressSpinnerModule,
  MatIconModule,
    MatButtonModule,
    MatAutocompleteModule,  
    NgSelectModule,
  MatCardModule,
  MatFormFieldModule,
  LayoutModule,
  MatToolbarModule,
  MatListModule
  ],
    providers: [
        [LoaderService],
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
