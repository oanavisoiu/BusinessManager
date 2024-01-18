import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Budget } from "src/app/shared/models/budget/budget.model";
import { BudgetType } from "src/app/shared/models/budget/budget-type.model";
import { DayBudget } from "src/app/shared/models/budget/day-budget.model";
import { UpdateBudget } from "src/app/shared/models/budget/update-budget.model";

@Injectable({
  providedIn: 'root'
})
export class BudgetService {

  baseApiUrl:string =environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getBudget(id:string){
    return this.http.get<Budget>(this.baseApiUrl+'/api/budget/get-budget/'+id);
  }
  getBudgets(id:string){
    return this.http.get<Budget[]>(this.baseApiUrl+'/api/budget/get-budgets-by-company-id/'+id);
  }
  addBudget(budget:Budget){
    budget.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Budget>(this.baseApiUrl+'/api/budget/add-budget',budget);
  }
  getPaymentTypeNames(){
    return this.http.get<string>(this.baseApiUrl+'/api/paymenttype/get-payment-type-names');
  }
  getBudgetTypeNames(){
    return this.http.get<string>(this.baseApiUrl+'/api/budgettype/get-budget-type-names');
  }
  getBudgetTypes(){
    return this.http.get<BudgetType>(this.baseApiUrl+'/api/budgettype/get-budget-types');
  }
  deleteBudget(id:string){
    return this.http.delete<Budget>(this.baseApiUrl+'/api/budget/delete-budget/'+id);
  }
  getDayBudgets(id:string){
    return this.http.get<DayBudget[]>(this.baseApiUrl+'/api/budget/get-day-budgets/'+id);
  }
  updateBudget(id:string, budget:UpdateBudget){
    return this.http.put<UpdateBudget>(this.baseApiUrl+'/api/budget/update-budget/'+id,budget);
  }
}
