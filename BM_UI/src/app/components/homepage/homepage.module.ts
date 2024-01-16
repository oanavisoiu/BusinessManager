import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DevextremeModule } from 'src/app/shared/materials/devextreme/devextreme.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { HomeComponent } from './home/home.component';
import { BirthdaysCardComponent } from './birthdays-card/birthdays-card.component';
import { BudgetsCardComponent } from './budgets-card/budgets-card.component';
import { ToDoCardComponent } from './to-do-card/to-do-card.component';
import { SetCompanyComponent } from './set-company/set-company.component';
import { BudgetModule } from '../budget/budget.module';
import { ChartCardComponent } from './chart-card/chart-card.component';



@NgModule({
  declarations: [
    HomeComponent,
    BirthdaysCardComponent,
    BudgetsCardComponent,
    ToDoCardComponent,
    SetCompanyComponent,
    ChartCardComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    DevextremeModule,
    BudgetModule
  ]
})
export class HomepageModule { }
