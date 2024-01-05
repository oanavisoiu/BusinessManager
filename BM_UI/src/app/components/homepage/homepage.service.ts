import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UpcomingBudget } from 'src/app/shared/models/budget/upcoming-budget';
import { EmployeeBirthday } from 'src/app/shared/models/employee/employee-birthday';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomepageService {

  baseApiUrl = environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getEmployeeBirthdays(id:string){
    return this.http.get<EmployeeBirthday[]>(this.baseApiUrl+'/api/companyemployee/get-employees-by-upcoming-birthdays/'+id);
  }
  getUpcomingBudgets(id:string){
    return this.http.get<UpcomingBudget[]>(this.baseApiUrl+'/api/budget/get-upcoming-budgets/'+id);
  }
}
