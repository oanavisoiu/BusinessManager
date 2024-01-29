import { SharedService } from './../../../shared/shared.service';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/account/user.model';
import { take } from 'rxjs';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css']
})
export class SendEmailComponent implements OnInit {

  emailForm:FormGroup = new FormGroup([]);
  submitted=false;
  mode:string|undefined;
  errorMessages:string[]=[];

  constructor(private accountService:AccountService,
    private sharedService:SharedService,
    private router:Router,
    private activatedRoute:ActivatedRoute,
    private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.accountService.user$.pipe(take(1)).subscribe({
      next:(user:User | null)=>{
        if(user){
          this.router.navigateByUrl('/');
        }else{
          const mode=this.activatedRoute.snapshot.paramMap.get('mode');
          if(mode){
            this.mode=mode;
            this.initializeForm();
          }
        }
      }
    });
  }
  initializeForm() {
    this.emailForm = this.formBuilder.group({
      email: [
        '',
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
      ]
    });
  }

  sendEmail()
  {
    this.submitted=true;
    this.errorMessages=[];

    if(this.emailForm.valid && this.mode){
      if(this.mode.includes('resend-email-confirmation-link')){
        this.accountService.resendEmailConfirmationLink(this.emailForm.get('email')?.value).subscribe({
          next:(response:any)=>{
            this.sharedService.showNotification(true,response.value.title,response.value.message);
            this.router.navigateByUrl('/account/login');
          },
          error:error=>{
            if(error.error.errors){
              this.errorMessages=error.error.errors;
            }
            else{
              this.errorMessages.push(error.error);
            }
          }
        });
      }
      else if(this.mode.includes('forgot-username-or-password')){
        this.accountService.forgotUsernameorPassword(this.emailForm.get('email')?.value).subscribe({
          next:(response:any)=>{
            this.sharedService.showNotification(true,response.value.title,response.value.message);
            this.router.navigateByUrl('/account/login');
          },
          error:error=>{
            if(error.error.errors){
              this.errorMessages=error.error.errors;
            }
            else{
              this.errorMessages.push(error.error);
            }
          }
        });
      }
    }
  }
  cancel(){
    this.router.navigateByUrl('/account/login');
  }

}
