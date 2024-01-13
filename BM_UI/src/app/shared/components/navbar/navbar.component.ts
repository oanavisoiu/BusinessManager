import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../components/account/account.service';
import { Router } from '@angular/router';
import { CompanyService } from 'src/app/components/company/company.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isDrawerOpen = true;
  elementAttr: any;
  toolbarContent = [{
    widget: 'dxButton',
    location: 'before',
    options: {
      icon: 'menu',
      onClick: () => this.isDrawerOpen = !this.isDrawerOpen,
    },
  }];


  constructor(public accountService:AccountService, public companyService:CompanyService){}


  ngOnInit(): void {
  }

}
