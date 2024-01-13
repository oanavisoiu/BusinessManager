import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../homepage/home/home.component';
import { AuthorizationGuard } from 'src/app/shared/guards/authorization.guard';

const routes: Routes=[
  {path:'',component:HomeComponent,canActivate:[AuthorizationGuard]},
  {path:'add-company',component:HomeComponent,canActivate:[AuthorizationGuard]},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CompanyRoutingModule { }
