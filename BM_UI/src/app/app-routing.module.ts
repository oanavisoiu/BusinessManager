import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { NotFoundComponent } from './shared/components/errors/not-found/not-found.component';
import { AuthorizationGuard } from './shared/guards/authorization.guard';

const routes: Routes = [
  { path: 'employees', component:EmployeesListComponent, canActivate:[AuthorizationGuard]},
  { path: 'employees/add', component: EmployeesListComponent, canActivate:[AuthorizationGuard] },
  { path: 'employees/view/:id', component: EmployeesListComponent, canActivate:[AuthorizationGuard] },
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
  { path: '**', component: NotFoundComponent, pathMatch:'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
