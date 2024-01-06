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
import { DevextremeModule } from './materials/devextreme/devextreme.module';
import { CompanyNotSetComponent } from './components/company-not-set/company-not-set.component';



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
    RouterModule,
    HttpClientModule,
    NgbModalModule,
    DevextremeModule
  ],
  exports: [
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    ValidationMessagesComponent,
    NgbModalModule,
    NavbarComponent,
    LoggedUserComponent,
    CompanyNotSetComponent
  ]
})
export class SharedModule { }
