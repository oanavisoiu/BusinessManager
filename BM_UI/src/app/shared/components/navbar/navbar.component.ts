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
  pageName='';
  elementAttr: any;
  toolbarContent = [{
    widget: 'dxButton',
    location: 'before',
    template: () => {
      return "<i class='m-2 bi bi-list custom-icon-size' style='font-size:30px;'></i>";
    },
      onClick: () => this.isDrawerOpen = !this.isDrawerOpen,
  },
  {
    template: () => {
      return "<p class='m-2' style=' font-style: italic; font-size:20px;'>BusinessManager</p>";
    },
    location: 'after'
  }];
  onOpenDrawer(){
    this.isDrawerOpen = !this.isDrawerOpen;
  }

  constructor(public accountService:AccountService, public companyService:CompanyService){}


  ngOnInit(): void {
  }

}
