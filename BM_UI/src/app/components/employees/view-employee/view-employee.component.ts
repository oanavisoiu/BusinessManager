
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Employee } from 'src/app/shared/models/employee.model';
import { EmployeesService } from 'src/app/services/employees/employees.service';

@Component({
  selector: 'app-view-employee',
  templateUrl: './view-employee.component.html',
  styleUrls: ['./view-employee.component.css']
})
export class ViewEmployeeComponent implements OnInit {

  employee: Employee={
    id:'',
    name:'',
    email:'',
    phone:0,
    salary:0,
    department:''

  };

  constructor(private employeeService:EmployeesService, private modalService:NgbModal,
    private router:Router) { }

  ngOnInit(): void {
  }

  onSaveEmployee(){
    this.employeeService.updateEmployee(this.employee.id,this.employee).subscribe({
      next:(response)=>{
      this.modalService.dismissAll();
      this.router.navigate(['employees']);
      }
    });
  }

  onDeleteEmployee(){
    this.employeeService.deleteEmployee(this.employee.id).subscribe({
      next:(response)=>{
        this.modalService.dismissAll();
        this.router.navigate(['employees']);
        }
    });
  }

  onButtonClose(){
    this.modalService.dismissAll();
    this.router.navigate(['employees']);
  }

}
