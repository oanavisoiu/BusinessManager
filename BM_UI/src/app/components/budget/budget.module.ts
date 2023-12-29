import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DevextremeModule } from 'src/app/shared/materials/devextreme/devextreme.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { BudgetRoutingModule } from './budget-routing.module';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    BudgetListComponent
  ],
  imports: [
    CommonModule,
    DevextremeModule,
    SharedModule,
    BudgetRoutingModule,
    FormsModule
  ]
})
export class BudgetModule { }
