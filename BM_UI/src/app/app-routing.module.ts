import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { AuthenticationComponent } from './components/authentication/authentication/authentication.component';


const routes: Routes = [
  {
    path:'',
    component: EmployeesListComponent
  },
  {
    path:'employees',
    component: EmployeesListComponent
  },
  {
    path:'employees/add',
    component: EmployeesListComponent
  },
  {
    path:'employees/view/:id',
    component: EmployeesListComponent
  },
  {
    path:'authentication/login',
    component: AuthenticationComponent
  },
  {
    path:'authentication',
    component: AuthenticationComponent
  },
  {
    path:'authentication/register',
    component: AuthenticationComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
