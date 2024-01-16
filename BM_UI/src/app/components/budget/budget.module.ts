import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { BudgetRoutingModule } from './budget-routing.module';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { ThirtyDaysChartComponent } from './thirty-days-chart/thirty-days-chart.component';



@NgModule({
  declarations: [
    BudgetListComponent,
    ThirtyDaysChartComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    BudgetRoutingModule
  ],
  exports: [
    ThirtyDaysChartComponent
  ],
  providers: [DatePipe]
})
export class BudgetModule { }
