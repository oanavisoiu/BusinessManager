import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddCompanyComponent } from './add-company/add-company.component';
import { CompanyRoutingModule } from './company-routing.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AddCompanyComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CompanyRoutingModule,
    FormsModule
  ]
})
export class CompanyModule { }
