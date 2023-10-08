import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees/employees.service';
import { EmployeesListComponent } from '../employees-list/employees-list.component';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

   employee: Employee={
    id:'',
    name:'',
    email:'',
    phone:0,
    salary:0,
    department:''

  };

  constructor(private employeeService:EmployeesService, private modalService:NgbModal) { }

  ngOnInit(): void {
  }

  onSaveEmployee(){
    this.employeeService.updateEmployee(this.employee.id,this.employee).subscribe({
      next:(response)=>{
      this.modalService.dismissAll();
      }
    });
  }

  onDeleteEmployee(){
    this.employeeService.deleteEmployee(this.employee.id).subscribe({});
  }


}
