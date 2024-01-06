import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DevextremeModule } from 'src/app/shared/materials/devextreme/devextreme.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { HomeComponent } from './home/home.component';
import { EventCardComponent } from './event-card/event-card.component';
import { BirthdaysCardComponent } from './birthdays-card/birthdays-card.component';
import { BudgetsCardComponent } from './budgets-card/budgets-card.component';
import { ToDoCardComponent } from './to-do-card/to-do-card.component';
import { SetCompanyComponent } from './set-company/set-company.component';



@NgModule({
  declarations: [
    HomeComponent,
    EventCardComponent,
    BirthdaysCardComponent,
    BudgetsCardComponent,
    ToDoCardComponent,
    SetCompanyComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    DevextremeModule
  ]
})
export class HomepageModule { }
