import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Budget } from "src/app/shared/models/budget/budget.model";

@Injectable({
  providedIn: 'root'
})
export class BudgetService {

  baseApiUrl:string =environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getBudget(id:string){
    return this.http.get<Budget[]>(this.baseApiUrl+'/api/budget/get-budgets-by-company-id/'+id);
  }
  addBudget(budget:Budget){
    budget.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Budget>(this.baseApiUrl+'/api/budget/add-budget',budget);
  }
  getPaymentTypeNames(){
    return this.http.get<string>(this.baseApiUrl+'/api/paymenttype/get-payment-type-names');
  }
  deleteBudget(id:string){
    return this.http.delete<Budget>(this.baseApiUrl+'/api/budget/delete-budget/'+id);
  }
}
