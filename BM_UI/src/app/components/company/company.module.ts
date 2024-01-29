import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddCompanyComponent } from './add-company/add-company.component';
import { CompanyRoutingModule } from './company-routing.module';
import { FormsModule } from '@angular/forms';
import { CompanyInfoComponent } from './company-info/company-info.component';
import { ProfileModule } from '../profile/profile.module';
import { ConfirmDeleteComponent } from './confirm-delete/confirm-delete.component';

@NgModule({
  declarations: [
    AddCompanyComponent,
    CompanyInfoComponent,
    ConfirmDeleteComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CompanyRoutingModule,
    FormsModule,
  ],
  exports:[
    CompanyInfoComponent
  ]
})
export class CompanyModule { }
