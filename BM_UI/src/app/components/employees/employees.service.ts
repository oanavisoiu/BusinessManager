import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { CompanyEmployee } from 'src/app/shared/models/employee/company-employee.model';
import { EmployeeUpdate } from 'src/app/shared/models/employee/employee-update.model';
import { Employee } from 'src/app/shared/models/employee/employee.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  baseApiUrl:string =environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getAllEmployees(companyId:string) : Observable<Employee[]>{
    return this.http.get<Employee[]>(this.baseApiUrl + '/api/companyemployee/get-employees-by-company/'+companyId)
  }

  addEmployee(employee:Employee) : Observable<Employee>{
    employee.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Employee>(this.baseApiUrl + '/api/employee/add-employee', employee);
  }
  addCompanyEmployee(companyEmployee:CompanyEmployee) : Observable<CompanyEmployee>{
    companyEmployee.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<CompanyEmployee>(this.baseApiUrl + '/api/companyemployee/add-company-employee', companyEmployee);
  }
  getEmployee(id:string):Observable<Employee>{
    return this.http.get<Employee>(this.baseApiUrl + '/api/employee/get-employee/'+ id);
  }
  updateEmployee(id:string, employee:EmployeeUpdate):Observable<EmployeeUpdate>{
    return this.http.put<EmployeeUpdate>(
      this.baseApiUrl + '/api/employee/update-employee/'+
      id, employee
    );
  }
  deleteEmployee(id:string):Observable<EmployeeUpdate>{
    return this.http.delete<EmployeeUpdate>(
      this.baseApiUrl + '/api/employee/delete-employee/' + id
    )
  }
  getSumOfSalaries(id:string,date:Date){
    return this.http.get<number>(this.baseApiUrl+'/api/companyemployee/get-sum-of-salaries/'+id+'/'+date);
  }

}
