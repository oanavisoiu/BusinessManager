import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/components/account/account.service';

@Component({
  selector: 'app-logged-user',
  templateUrl: './logged-user.component.html',
  styleUrls: ['./logged-user.component.css']
})
export class LoggedUserComponent implements OnInit {

  constructor(public accountService:AccountService) {}

  ngOnInit(): void {}

}
