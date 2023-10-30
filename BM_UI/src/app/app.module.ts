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
import { HomeComponent } from './components/homepage/home/home.component';
import { AccountModule } from './components/account/account.module';
import { SharedModule } from './shared/shared.module';
import { ConfirmEmailComponent } from './components/account/confirm-email/confirm-email.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesListComponent,
    AddEmployeeComponent,
    ViewEmployeeComponent,
    HomeComponent,
    ConfirmEmailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    AccountModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
