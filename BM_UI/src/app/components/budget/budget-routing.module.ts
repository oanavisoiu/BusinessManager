import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { AuthorizationGuard } from 'src/app/shared/guards/authorization.guard';


const routes: Routes=[
  { path: '', component:BudgetListComponent, canActivate:[AuthorizationGuard]}
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
export class BudgetRoutingModule { }
