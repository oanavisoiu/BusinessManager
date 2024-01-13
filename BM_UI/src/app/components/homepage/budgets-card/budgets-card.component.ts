import { Component, OnInit } from '@angular/core';
import { UpcomingBudget } from 'src/app/shared/models/budget/upcoming-budget';
import { CompanyService } from '../../company/company.service';
import { HomepageService } from '../homepage.service';
import { Company } from 'src/app/shared/models/company/company.model';

@Component({
  selector: 'app-budgets-card',
  templateUrl: './budgets-card.component.html',
  styleUrls: ['./budgets-card.component.css']
})
export class BudgetsCardComponent implements OnInit {

  budgets:UpcomingBudget[]=[];
  constructor(public companyService: CompanyService, private homepageService: HomepageService) { }

  ngOnInit(): void {
    this.getUpcomingBudgets();
  }

  getUpcomingBudgets() {
    this.companyService.company$.subscribe({
      next: (company: Company | null) => {
        if (company) {
          this.homepageService.getUpcomingBudgets(company.id).subscribe({
            next: (b) => {
              this.budgets = b;
            },
            error:(err)=>{
              console.log(err);
            }
          });
        }
      },
    });
  }
}
