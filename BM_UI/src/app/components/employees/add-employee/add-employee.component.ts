import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Employee } from 'src/app/shared/models/employee.model';
import { EmployeesService } from 'src/app/services/employees/employees.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  employee:Employee={
    id:'',
    name:'',
    email:'',
    phone:0,
    salary:0,
    department:''
  };
  constructor(private employeeService:EmployeesService, private modalService:NgbModal, private route:ActivatedRoute,
    private router:Router) { }

  ngOnInit(): void {
  }

  addEmployee(){
    this.employeeService.addEmployee(this.employee).subscribe({
      next:(tmpEmployee)=>{
        console.log(tmpEmployee);
        this.modalService.dismissAll();
        this.router.navigate(['employees']);
      }
    })
  }
  onButtonClose(){
    this.modalService.dismissAll();
    this.router.navigate(['employees']);
  }

}
