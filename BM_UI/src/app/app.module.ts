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
import { NavbarComponent } from './components/homepage/navbar/navbar.component';

import { NotFoundComponent } from './shared/components/errors/not-found/not-found.component';
import { ValidationMessagesComponent } from './shared/components/errors/validation-messages/validation-messages.component';
import { HomeComponent } from './components/homepage/home/home.component';
import { AccountComponent } from './components/account/account/account.component';
import { AccountModule } from './components/account/account.module';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesListComponent,
    AddEmployeeComponent,
    ViewEmployeeComponent,
    NavbarComponent,
    NotFoundComponent,
    ValidationMessagesComponent,
    HomeComponent,
    AccountComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    AccountModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
