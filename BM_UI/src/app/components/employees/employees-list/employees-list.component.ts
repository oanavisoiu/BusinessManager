import { EmployeesService } from '../employees.service';
import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../company/company.service';
import { Employee } from 'src/app/shared/models/employee/employee.model';
import CustomStore from 'devextreme/data/custom_store';
import { catchError, of } from 'rxjs';
import { CompanyEmployee } from 'src/app/shared/models/employee/company-employee.model';
import { exportDataGrid } from 'devextreme/pdf_exporter';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements OnInit {

  employees: any;
  errors="";
  maxDate: Date = new Date();
  minDate:Date = new Date();
  companyEmployee: CompanyEmployee = {
                id: '',
                companyId: '',
                employeeId: ''
              };

  constructor(private employeesService: EmployeesService, public companyService:CompanyService) {
    this.maxDate = new Date(this.maxDate.setFullYear(this.maxDate.getFullYear() - 15));
  }

  ngOnInit(): void {

    this.operations();
  }

  operations() {
    this.companyService.company$.subscribe(
      (company) => {
        if (company) {
          const dataService=this.employeesService;
          this.employees = new CustomStore({
            key: 'id',
            load: () => {
              return dataService.getAllEmployees(company.id).pipe(
                catchError((error) => {
                  return of([]);
                })
              ).toPromise();

            },
            update: (key, values) => {
              const employeeId = key;
              return new Promise((resolve, reject) => {
                dataService.getEmployee(employeeId).subscribe({
                  next: (employee) => {
                    Object.assign(employee, values);
                    const { id, ...updatedValues } = employee;
                    const updatedEmployee=updatedValues;
                    dataService.updateEmployee(employeeId, updatedEmployee).subscribe({
                      next: (result) => {
                        resolve(employee);
                      },
                      error: (error) => {
                        reject(error);
                      }
                    });
                  },
                  error: (error) => {
                    reject(error);
                  }
                });
              });
            },
            insert: (values:any) => {
              return new Promise<Employee>((resolve, reject) => {
                const newEmployee: Employee = { ...values };

                dataService.addEmployee(newEmployee).subscribe({
                  next: (emp: Employee) => {
                    this.companyEmployee.employeeId = emp.id;
                    this.companyEmployee.companyId = company.id;

                    dataService.addCompanyEmployee(this.companyEmployee).subscribe({
                      next: (company) => {
                        resolve(emp);
                      },
                      error: (companyError) => {
                        reject(companyError);
                      },
                    });
                  },
                  error: (empError) => {
                    reject(empError);
                  },
                });
              });
            },
            remove: (key) => {
              return new Promise<void>((resolve, reject) => {
                dataService.deleteEmployee(key).subscribe({
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
  onExporting(e:any) {
    const doc = new jsPDF();
    exportDataGrid({
      jsPDFDocument: doc,
      component: e.component,
    }).then(() => {
      doc.save('Employees.pdf');
    });
  }

}
