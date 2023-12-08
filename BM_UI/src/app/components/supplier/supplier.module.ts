import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupplierListComponent } from './supplier-list/supplier-list/supplier-list.component';
import { SupplierRoutingModule } from './supplier-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DevextremeModule } from 'src/app/shared/materials/devextreme/devextreme.module';



@NgModule({
  declarations: [
    SupplierListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    SupplierRoutingModule,
    DevextremeModule
  ]
})
export class SupplierModule { }
