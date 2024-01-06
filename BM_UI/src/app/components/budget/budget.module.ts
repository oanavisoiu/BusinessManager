import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { DevextremeModule } from 'src/app/shared/materials/devextreme/devextreme.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { BudgetRoutingModule } from './budget-routing.module';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { FormsModule } from '@angular/forms';
import { ThirtyDaysChartComponent } from './thirty-days-chart/thirty-days-chart.component';



@NgModule({
  declarations: [
    BudgetListComponent,
    ThirtyDaysChartComponent
  ],
  imports: [
    CommonModule,
    DevextremeModule,
    SharedModule,
    BudgetRoutingModule,
    FormsModule
  ],
  exports: [
    ThirtyDaysChartComponent
  ],
  providers: [DatePipe]
})
export class BudgetModule { }
