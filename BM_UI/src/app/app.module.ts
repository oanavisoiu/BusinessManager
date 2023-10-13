import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { HttpClientModule } from '@angular/common/http';
import { AddEmployeeComponent } from './components/employees/add-employee/add-employee.component';
import { FormsModule } from '@angular/forms';

import { ViewEmployeeComponent } from './components/employees/view-employee/view-employee.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './components/authentication/login/login.component';
import { NavbarComponent } from './components/homepage/navbar/navbar.component';
import { AuthenticationComponent } from './components/authentication/authentication/authentication.component';
import { RegisterComponent } from './components/authentication/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesListComponent,
    AddEmployeeComponent,
    ViewEmployeeComponent,
    LoginComponent,
    NavbarComponent,
    AuthenticationComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
