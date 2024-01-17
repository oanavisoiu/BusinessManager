import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ValidationMessagesComponent } from './components/errors/validation-messages/validation-messages.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { NotificationComponent } from './components/modals/notification/notification.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoggedUserComponent } from './components/logged-user/logged-user.component';
import { CompanyNotSetComponent } from './components/company-not-set/company-not-set.component';
import { DxButtonModule, DxChartModule, DxDataGridModule, DxDateBoxModule, DxDrawerModule, DxFormModule, DxLookupModule, DxNumberBoxModule, DxSchedulerModule, DxToolbarModule } from 'devextreme-angular';



@NgModule({
  declarations: [
    NotFoundComponent,
    ValidationMessagesComponent,
    NotificationComponent,
    NavbarComponent,
    LoggedUserComponent,
    CompanyNotSetComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModalModule,
    DxDrawerModule,
    DxToolbarModule,
    RouterModule
  ],
  exports: [
    ValidationMessagesComponent,
    NavbarComponent,
    LoggedUserComponent,
    CompanyNotSetComponent,
    RouterModule,
    DxDataGridModule,
    DxChartModule,
    DxLookupModule,
    DxNumberBoxModule,
    DxSchedulerModule,
    DxButtonModule,
    DxFormModule,
    DxDateBoxModule
  ]
})
export class SharedModule { }
