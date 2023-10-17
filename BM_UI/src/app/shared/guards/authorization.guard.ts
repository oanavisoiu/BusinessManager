import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from 'src/app/components/account/account.service';
import { SharedService } from '../shared.service';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationGuard{

  constructor(private accountService:AccountService, private sharedService:SharedService,private router:Router){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean>{
    return this.accountService.user$.pipe(
      map((user:User | null) =>
      {
        if(user){
          return true;
        }
        else{
          this.sharedService.showNotification(false,'Restricted area', 'Leavve now');
          this.router.navigate(['account/login'], {queryParams: {returnUrl:state.url}});
          return false;
        }
      }
      )
    );
  }



}
