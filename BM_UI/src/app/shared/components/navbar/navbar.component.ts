import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../components/account/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {


  showSubmenuModes: string[] = ['slide', 'expand'];

  positionModes: string[] = ['left', 'right'];

  showModes: string[] = ['push', 'shrink', 'overlap'];
  selectedOpenMode = 'shrink';

  selectedPosition = 'left';

  selectedRevealMode = 'slide';

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
  menuItems = [
    { text: 'Employee', route: '/employees' },
    { text: 'Stats', page: '' },
    { text: 'Stats', page: 'stats' },
    { text: 'Stats', page: 'stats' },
    { text: 'Stats', page: 'stats' },
    { text: 'Stats', page: 'stats' }
  ];
  constructor(public accountService:AccountService, private router:Router) { }

  ngOnInit(): void {
  }

  logout()
  {
    this.accountService.logout();
  }

  itemClick(e:any){

  }
}
