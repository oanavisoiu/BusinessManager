import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Company } from 'src/app/shared/models/company/company.model';
import { CompanyService } from '../company.service';
import { AccountService } from '../../account/account.service';
import { User } from 'src/app/shared/models/account/user.model';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.css']
})
export class AddCompanyComponent implements OnInit {

  company:Company={
    id:'',
    userId:'',
    name:'',
    address:'',
    phoneNumber:''
  };
  constructor(private companyService:CompanyService, private modalService:NgbModal, private router:Router,private accountService:AccountService) { }

  ngOnInit(): void {
  }

  addCompany(){
    this.companyService.addCompany(this.company)?.subscribe({

      next:(tmpCompany)=>{
        console.log(this.company);
        this.modalService.dismissAll();
        this.router.navigate(['']);
      },
      error:(err)=>{
        console.log(this.company);
        console.log(err);
      }
    });
    this.accountService.user$.subscribe({
      next:(user:User|null)=>{}
    })
  }
  onButtonClose(){
    this.modalService.dismissAll();
    this.router.navigate(['']);
  }

  saveButtonOptions = {
    text: 'Save',
    width: '120px',
    useSubmitBehavior: true,
  };

}
