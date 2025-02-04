import { ToDoView } from 'src/app/shared/models/to-do/to-do-view.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UpcomingBudget } from 'src/app/shared/models/budget/upcoming-budget.model';
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
  getTodayToDos(id:string){
    return this.http.get<ToDoView[]>(this.baseApiUrl+'/api/todo/get-today-to-dos/'+id);
  }
  getUpcomingToDos(id:string){
    return this.http.get<ToDoView[]>(this.baseApiUrl+'/api/todo/get-upcoming-to-dos/'+id);
  }
}
