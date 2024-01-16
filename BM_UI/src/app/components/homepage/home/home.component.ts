import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../account/account.service';
import { CompanyService } from '../../company/company.service';

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
    public companyService: CompanyService,
  ) {}

  ngOnInit(): void {
  }




}
