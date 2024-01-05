import { Component, OnInit } from '@angular/core';
import { AccountService } from './components/account/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(public accountService:AccountService){}

  ngOnInit(): void {
      this.refreshUser();
  }

  refreshUser(){
      this.accountService.refreshUser()?.subscribe({
        next: _=>{},
        error: _=>{
          this.accountService.logout();
        }
      });
    }
  }
