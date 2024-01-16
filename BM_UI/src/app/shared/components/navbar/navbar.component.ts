import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../components/account/account.service';
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
  onOpenDrawer(){
    this.isDrawerOpen = !this.isDrawerOpen;
  }

  constructor(public accountService:AccountService, public companyService:CompanyService){}


  ngOnInit(): void {
  }

  onLogout(){
    this.accountService.logout();
  }
}
