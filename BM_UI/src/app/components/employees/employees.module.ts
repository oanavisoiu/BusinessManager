import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesListComponent } from './employees-list/employees-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { EmployeesRoutingModule } from './employees-routing.module';
import { FormsModule } from '@angular/forms';
import { DevExtremeModule, DxDataGridComponent } from 'devextreme-angular';

@NgModule({
  declarations: [
    EmployeesListComponent,

  ],
  imports: [
    CommonModule,
    SharedModule,
    EmployeesRoutingModule,
    FormsModule,

  ],
})
export class EmployeesModule { }
