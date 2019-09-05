import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { PartComponent } from './dashboard/part/part.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './auth/auth.guard';
import { FixtureComponent } from './dashboard/fixture/fixture.component';
import { PurchaseorderComponent } from './dashboard/purchaseorder/purchaseorder.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'Login' }
  },
  {
    path: '',                       // {1}
    component: DashboardComponent,
    canActivate: [AuthGuard],
        // {2}
    children: [
      {
        path: 'dashboard/part',
        component: PartComponent,
        data: { title: 'Part' }
      },
      {
        path: 'dashboard/fixture',
        component: FixtureComponent,
        data: { title: 'Fixture' }
      },
      {
        path: 'dashboard/purchaseorder',
        component: PurchaseorderComponent,
        data: { title: 'Purchase Order' }
      }
    ]
  },

  {
    path: 'reg',
    component: RegisterComponent,
    data: { title: 'Register' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
