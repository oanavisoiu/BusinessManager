import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { HomeComponent } from './components/homepage/home/home.component';
import { NotFoundComponent } from './shared/components/errors/not-found/not-found.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'account',
    loadChildren: () =>
      import('./components/account/account.module').then(
        (module) => module.AccountModule
      ),
  },
  { path: 'employees', component: EmployeesListComponent },
  { path: 'employees/add', component: EmployeesListComponent },
  { path: 'employees/view/:id', component: EmployeesListComponent },
  { path: '**', component: NotFoundComponent, pathMatch:'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
