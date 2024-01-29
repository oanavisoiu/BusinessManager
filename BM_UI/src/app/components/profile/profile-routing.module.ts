import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { AuthorizationGuard } from 'src/app/shared/guards/authorization.guard';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes=[
  {path:'',component:ProfilePageComponent,canActivate:[AuthorizationGuard]}
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
export class ProfileRoutingModule { }
