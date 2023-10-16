import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Employee } from 'src/app/shared/models/employee.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  baseApiUrl:string =environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getAllEmployees() : Observable<Employee[]>{
    return this.http.get<Employee[]>(this.baseApiUrl + '/api/employees')
  }

  addEmployee(employee:Employee) : Observable<Employee>{
    employee.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Employee>(this.baseApiUrl + '/api/employees', employee);
  }
  getEmployee(id:string):Observable<Employee>{
    return this.http.get<Employee>(this.baseApiUrl + '/api/employees'+ id);
  }
  updateEmployee(id:string, employee:Employee):Observable<Employee>{
    return this.http.put<Employee>(
      this.baseApiUrl + '/api/employees/'+
      id, employee
    );
  }
  deleteEmployee(id:string):Observable<Employee>{
    return this.http.delete<Employee>(
      this.baseApiUrl + '/api/employees/' + id
    )
  }

}
