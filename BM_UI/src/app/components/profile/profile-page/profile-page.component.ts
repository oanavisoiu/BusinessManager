import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../company/company.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  constructor(public companyService:CompanyService) { }

  ngOnInit(): void {
  }

}
