import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../account/account.service';
import { User } from 'src/app/shared/models/account/user.model';
import { take } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { AddCompanyComponent } from '../../company/add-company/add-company.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CompanyService } from '../../company/company.service';
import { Company } from 'src/app/shared/models/company/company.model';
import { HomepageService } from '../homepage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  birthdays: any;
  budgets: any;
  constructor(
    public accountService: AccountService,
    private modalService: NgbModal,
    public companyService: CompanyService,
    private homepageService: HomepageService
  ) {}

  ngOnInit(): void {
  }




}
