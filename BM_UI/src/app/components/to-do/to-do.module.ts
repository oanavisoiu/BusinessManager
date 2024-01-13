import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoCalendarComponent } from './to-do-calendar/to-do-calendar.component';
import { ToDoRoutingModule } from './to-do-routing.module';
import { DevextremeModule } from 'src/app/shared/materials/devextreme/devextreme.module';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [
    ToDoCalendarComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    ToDoRoutingModule,
    DevextremeModule
  ]
})
export class ToDoModule { }
