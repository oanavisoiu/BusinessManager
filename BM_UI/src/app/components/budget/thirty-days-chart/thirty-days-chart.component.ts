import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../company/company.service';
import { BudgetService } from '../budget.service';
import { DatePipe } from '@angular/common';
import { DayBudget } from 'src/app/shared/models/budget/day-budget.model';

@Component({
  selector: 'app-thirty-days-chart',
  templateUrl: './thirty-days-chart.component.html',
  styleUrls: ['./thirty-days-chart.component.css']
})
export class ThirtyDaysChartComponent implements OnInit {

  dataSource :any;

  constructor(private companyService:CompanyService, private budgetService:BudgetService,private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.getDayBudgets();
  }
  formatDate(budgets: DayBudget[]): DayBudget[] {
    return budgets.map(budget => {
      if (budget.date) { // Check if date is not null
        const formattedDate = this.datePipe.transform(new Date(budget.date), 'MM/d/yyyy');
        budget.date = formattedDate ? new Date(formattedDate) : budget.date;
      }
      return budget;
    });
  }
  getDayBudgets(){
    this.companyService.company$.subscribe({
      next:(company)=>{
        if(company){
          this.budgetService.getDayBudgets(company?.id).subscribe({
            next: (budgets) => {
              //this.dataSource = budgets;
              this.dataSource = this.formatDate(budgets);
              console.log('Data Source:', this.dataSource);
            },
            error: (err) => {
              console.error('Error fetching data:', err);
            }
          });
        }

      }
    });
  }

}
