import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesListComponent } from './employees-list/employees-list.component';
import { AuthorizationGuard } from 'src/app/shared/guards/authorization.guard';


const routes: Routes=[
  { path: '', component:EmployeesListComponent, canActivate:[AuthorizationGuard]},
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
export class EmployeesRoutingModule { }
