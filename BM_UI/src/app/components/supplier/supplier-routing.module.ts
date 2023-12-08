import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierListComponent } from './supplier-list/supplier-list/supplier-list.component';
import { AuthorizationGuard } from 'src/app/shared/guards/authorization.guard';
const routes: Routes=[
  { path: '', component:SupplierListComponent, canActivate:[AuthorizationGuard]},
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
export class SupplierRoutingModule { }
