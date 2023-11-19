import { EmployeesService } from './../../../services/employees/employees.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/shared/models/employee.model';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AddEmployeeComponent } from '../add-employee/add-employee.component';
import { ViewEmployeeComponent } from '../view-employee/view-employee.component';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements OnInit {

  employees: Employee[]=[];
  constructor(private employeesService: EmployeesService, private modalService: NgbModal) {
    this.reloadData();
  }

  ngOnInit(): void {
    this.reloadData();
  }

  openEditModal(employee:Employee){
    const modalOptions:NgbModalOptions={
      backdrop:'static',
      keyboard:false
    }
    const modalRef = this.modalService.open(ViewEmployeeComponent,modalOptions);
    modalRef.componentInstance.employee = { ...employee };
  }

  openAddEmployeeModal(){
    const modalOptions:NgbModalOptions={
      backdrop:'static',
      keyboard:false
    };
    const modalRef = this.modalService.open(AddEmployeeComponent,modalOptions);
  }

  reloadData(){
    this.employeesService.getAllEmployees().subscribe({
      next:(employees) =>
      {
        this.employees=employees;
      },
      error:(response) =>
      {
        console.log(response);
      }
    })
  }
}
