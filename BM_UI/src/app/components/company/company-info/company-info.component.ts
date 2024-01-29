import { Component, OnInit } from '@angular/core';
import { Company } from 'src/app/shared/models/company/company.model';
import { AccountService } from '../../account/account.service';
import { CompanyService } from '../company.service';
import { UpdateCompany } from 'src/app/shared/models/company/update-company.model';
import { User } from 'src/app/shared/models/account/user.model';
import { SharedService } from 'src/app/shared/shared.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmDeleteComponent } from '../confirm-delete/confirm-delete.component';

@Component({
  selector: 'app-company-info',
  templateUrl: './company-info.component.html',
  styleUrls: ['./company-info.component.css']
})
export class CompanyInfoComponent implements OnInit {

  companyForm:UpdateCompany={
    name:'',
    address:'',
    phoneNumber:''
  };
  constructor(public companyService:CompanyService, private accountService:AccountService, private sharedService:SharedService, private modal:NgbModal) { }

  ngOnInit(): void {
    this.companyService.company$.subscribe({
      next:(company:Company|null)=>{
        if(company){
          this.companyForm.name=company.name;
          this.companyForm.address=company.address;
          this.companyForm.phoneNumber=company.phoneNumber;
        }
      },
      error:(err)=>{

      }
    })
  }

  updateCompany(){
    this.companyService.updateCompany(this.companyForm)?.subscribe({

      next:(tmpCompany)=>{
        this.sharedService.showNotification(true, "Success", "Company updated successfully.");
      },
      error:(err)=>{
      }
    });
  }
  deleteCompany(){
    this.modal.open(ConfirmDeleteComponent);
  }
}
