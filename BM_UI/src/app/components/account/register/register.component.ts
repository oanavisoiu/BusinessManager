import { SharedService } from './../../../shared/shared.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { User } from 'src/app/shared/models/account/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  submitted = false;
  errorMessages: string[] = [];
  returnUrl:string='';

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private sharedService:SharedService,
    private activatedRoute:ActivatedRoute,
    private router:Router
  ) {
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

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(15),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(15),
        ],
      ],
      email: [
        '',
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(15),
        ],
      ],
    });
  }
  register() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.registerForm.valid) {
      this.accountService.register(this.registerForm.value).subscribe({
        next: (response:any) => {
          this.sharedService.showNotification(true, response.value.title,response.value.message);
          this.router.navigate(['/account/login']);
        },
        error: (error) => {
          if(error.error.errors){
            this.errorMessages=error.error.errors;
          }
          else{
            this.errorMessages.push(error.error);
          }
        },
      });
    }
  }
  onBack(){
    this.router.navigate(['/account']);
  }
}
