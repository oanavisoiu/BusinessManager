import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../company/company.service';
import { HomepageService } from '../homepage.service';
import { Company } from 'src/app/shared/models/company/company.model';
import { EmployeeBirthday } from 'src/app/shared/models/employee/employee-birthday';

@Component({
  selector: 'app-birthdays-card',
  templateUrl: './birthdays-card.component.html',
  styleUrls: ['./birthdays-card.component.css']
})
export class BirthdaysCardComponent implements OnInit {

  birthdays:EmployeeBirthday[]=[];
  constructor(public companyService: CompanyService, private homepageService: HomepageService) { }

  ngOnInit(): void {
    this.getBirthdays();
  }
  getBirthdays() {
    this.companyService.company$.subscribe({
      next: (company: Company | null) => {
        if (company) {
          this.homepageService.getEmployeeBirthdays(company.id).subscribe({
            next: (eb) => {
              this.birthdays = eb;
            },
            error:(err)=>{

            }
          });
        }
      },
    });
  }
}
