import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountModule } from './components/account/account.module';
import { SharedModule } from './shared/shared.module';
import { ConfirmEmailComponent } from './components/account/confirm-email/confirm-email.component';
import { CompanyModule } from './components/company/company.module';
import { EmployeesModule } from './components/employees/employees.module';
import { JwtInterceptor } from './shared/jwt.interceptor';
import { DevextremeModule } from './shared/materials/devextreme/devextreme.module';
import { SupplierModule } from './components/supplier/supplier.module';
import { BudgetModule } from './components/budget/budget.module';
import { HomepageModule } from './components/homepage/homepage.module';
import { ToDoModule } from './components/to-do/to-do.module';


@NgModule({
  declarations: [
    AppComponent,
    ConfirmEmailComponent
    ConfirmEmailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    AccountModule,
    SharedModule,
    CompanyModule,
    EmployeesModule,
    DevextremeModule,
    SupplierModule,
    BudgetModule,
    HomepageModule,
    ToDoModule
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,
      useClass:JwtInterceptor,
      multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
