import { Budget } from './../../../shared/models/budget/budget.model';
import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../budget.service';
import { CompanyService } from '../../company/company.service';
import CustomStore from 'devextreme/data/custom_store';
import { catchError, of, switchMap } from 'rxjs';
import { exportDataGrid } from 'devextreme/pdf_exporter';
import jsPDF from 'jspdf';
import { EmployeesService } from '../../employees/employees.service';
import { DatePipe } from '@angular/common';
import Decimal from 'decimal.js';

@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css'],
})

export class BudgetListComponent implements OnInit {
  budgets: any;
  paymentTypes: any;
  budgetTypes: any;
  isBudgetTypeNameSalaries: boolean = false;
  todayDate: any;
  formData:any;
  sumOfSalaries=0;
  currentDate=new Date();

  constructor(
    private budgetService: BudgetService,
    public companyService: CompanyService,
    private employeesService:EmployeesService,
    private datePipe: DatePipe){}

  ngOnInit(): void {
    this.initializeForm();
    this.getBudgetTypeNames();
    this.getPaymentTypeNames();
    this.todayDate = new Date().toLocaleString();
    this.operations();
  }
  initializeForm(){
    this.formData={
      budgetType:'',
      name:'',
      date:new Date(),
      createdDate:new Date(),
      value:0,
      paymentType:''
    };
  }
  formatDate(date:any):any{
    date = this.datePipe.transform(new Date(date), 'yyyy-MM-dd');
    return date;
  }
  getBudgetTypeNames(){
    this.budgetService.getBudgetTypeNames().subscribe({
      next: (bt) => {
        this.budgetTypes = bt;
      },
    });
  }
  getPaymentTypeNames(){
    this.budgetService.getPaymentTypeNames().subscribe({
      next: (pt) => {
        this.paymentTypes = pt;
      },
    });
  }
  changeBudgetType(event:any){
    this.formData.budgetType = event.value;
    this.isBudgetTypeNameSalaries = this.formData.budgetType === 'Salaries';
    if(this.isBudgetTypeNameSalaries){
      this.formData.value = this.sumOfSalaries;
      this.formData.paymentType='Expense';
    }
  }
  updateFormData(budget:Budget) {
    this.formData = {
        budgetType: budget.budgetTypeName || '',
        name: budget.name || '',
        date: budget.date ? new Date(budget.date) : new Date(),
        createdDate: budget.createdDate,
        value: budget.value || 0,
        paymentType: budget.paymentTypeName || ''
    };
}
  changeBudgetDate(event: any) {
    if (event.value) {
        this.formData.date = this.formatDate(event.value);
        this.companyService.company$.pipe(
            switchMap(company => {
                if (company) {
                    return this.employeesService.getSumOfSalaries(company.id, this.formData.date);
                } else {
                    return of(0);
                }
            })
        ).subscribe(sum => {
            this.sumOfSalaries = sum;
            if (this.isBudgetTypeNameSalaries) {
                this.formData = { ...this.formData, value: this.sumOfSalaries };
            }
        });
    }
}
  operations() {
    this.companyService.company$.subscribe((company) => {
      if (company) {
        this.currentDate=this.formatDate(this.currentDate);
        this.employeesService.getSumOfSalaries(company.id,this.currentDate).subscribe({
          next:(sum)=>{
            this.sumOfSalaries=sum;
          }
        })
        const dataService = this.budgetService;
        this.budgets = new CustomStore({
          key: 'id',
          load: () => {
            return dataService
              .getBudgets(company.id)
              .pipe(
                catchError((error) => {
                  return of([]);
                })
              )
              .toPromise();
          },
          insert: (values: any) => {
            return new Promise<Budget>((resolve, reject) => {
              const newBudget: Budget = { ...values };
              newBudget.companyId = company.id;
              newBudget.budgetTypeName=this.formData.budgetType;
              newBudget.date = this.formData.date;
              newBudget.value=this.formData.value;
              newBudget.paymentTypeName=this.formData.paymentType;
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
                },
              });
            });
          },
        });
      }
    });

  }
  onExporting(e:any) {
    const doc = new jsPDF();
    exportDataGrid({
      jsPDFDocument: doc,
      component: e.component,
      indent: 5,
    }).then(() => {
      doc.save('Budgets.pdf');
    });
  }
}
