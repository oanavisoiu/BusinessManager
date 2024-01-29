import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';
import { CompanyModule } from '../company/company.module';
import { ProfilePageComponent } from './profile-page/profile-page.component';



@NgModule({
  declarations: [
    ProfilePageComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProfileRoutingModule,
    CompanyModule,
  ]
})
export class ProfileModule { }
