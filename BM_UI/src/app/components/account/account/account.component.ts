import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RegisterComponent } from '../register/register.component';
import { AccountService } from '../account.service';
import { take } from 'rxjs';
import { User } from 'src/app/shared/models/account/user.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  returnUrl:string='';
  constructor(private modalService:NgbModal,private accountService: AccountService,private router:Router,
    private activatedRoute:ActivatedRoute) { }

  ngOnInit(){
    this.accountService.user$.pipe(take(1)).subscribe({
      next: (user:User | null) =>{
        if(user){
        this.router.navigateByUrl('/');
      }else{
        this.activatedRoute.queryParamMap.subscribe({
            next:(params:any)=>{
              if(params){
                return this.returnUrl = params.get('returnUrl');
              }
            }
        })
      }
      }
    });
  }

  onRegister(){
    const modalRef = this.modalService.open(RegisterComponent);
  }

}
