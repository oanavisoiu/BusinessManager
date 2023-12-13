import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../budget.service';
import { CompanyService } from '../../company/company.service';
import CustomStore from 'devextreme/data/custom_store';
import { catchError, of } from 'rxjs';
import { Budget } from 'src/app/shared/models/budget/budget.model';

@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css']
})
export class BudgetListComponent implements OnInit {

  budgets:any;
  paymentTypes:any;
  constructor(private budgetService:BudgetService, private companyService:CompanyService) { }

  ngOnInit(): void {
    this.budgetService.getPaymentTypeNames().subscribe({
      next:(pt)=>{
        this.paymentTypes=pt;
      }
    });

    this.operations();
  }

  operations() {
    this.companyService.company$.subscribe(
      (company) => {
        if (company) {
          const dataService=this.budgetService;
          this.budgets = new CustomStore({
            key: 'id',
            load: () => {
              return dataService.getBudget(company.id).pipe(
                catchError((error) => {
                  console.error('Error loading expenses:', error);
                  return of([]);
                })
              ).toPromise()
            },
            insert: (values:any) =>{
              console.log(values);
              return new Promise<Budget>((resolve, reject) => {
                const newBudget: Budget = { ...values };
                newBudget.companyId = company.id;
                dataService.addBudget(newBudget).subscribe({
                  next: (budget: Budget) => {
                    resolve(budget);
                  },
                  error: (empError) => {
                    reject(empError);
                  },
                });
              });
            },
            remove: (key) => {
              return new Promise<void>((resolve, reject) => {
                dataService.deleteBudget(key).subscribe({
                  next: () => {
                    resolve();
                  },
                  error: (err) => {
                    reject(err);
                  }
                });
              });
            }
          });
        }
      }
    );
  }
}
