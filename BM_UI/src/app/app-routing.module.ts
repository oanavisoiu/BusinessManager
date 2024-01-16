import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/errors/not-found/not-found.component';
import { AuthorizationGuard } from './shared/guards/authorization.guard';

const routes: Routes = [
  {
    path: 'employees',
    loadChildren: () =>
      import('./components/employees/employees.module').then(
        (module) => module.EmployeesModule
      ),
    canActivate:[AuthorizationGuard]
  },
  {
    path: '',
    loadChildren: () =>
      import('./components/company/company.module').then(
        (module) => module.CompanyModule
      ),
    canActivate:[AuthorizationGuard]
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./components/account/account.module').then(
        (module) => module.AccountModule
      ),
  },
  {
    path: 'suppliers',
    loadChildren: () =>
      import('./components/supplier/supplier.module').then(
        (module) => module.SupplierModule
      ),
    canActivate:[AuthorizationGuard]
  },
  {
    path: 'budget',
    loadChildren: () =>
      import('./components/budget/budget.module').then(
        (module) => module.BudgetModule
      ),
    canActivate:[AuthorizationGuard]
  },
  {
    path: 'to-do',
    loadChildren: () =>
      import('./components/to-do/to-do.module').then(
        (module) => module.ToDoModule
      ),
    canActivate:[AuthorizationGuard]
  },
  { path: '**', component: NotFoundComponent, pathMatch:'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
