import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoCalendarComponent } from './to-do-calendar/to-do-calendar.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/shared/guards/authorization.guard';


const routes: Routes=[
  { path: '', component:ToDoCalendarComponent, canActivate:[AuthorizationGuard]},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
  ]
})
export class ToDoRoutingModule { }
