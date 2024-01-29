import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CompanyService } from '../company.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-confirm-delete',
  templateUrl: './confirm-delete.component.html',
  styleUrls: ['./confirm-delete.component.css']
})
export class ConfirmDeleteComponent implements OnInit {

  constructor(private modal:NgbModal,private companyService:CompanyService, private route:Router) { }

  ngOnInit(): void {
  }
  onClose(){
    this.modal.dismissAll();
  }
  onOk(){
    this.companyService.deleteCompany()?.subscribe({
      next:(comp)=>{
        this.modal.dismissAll();
        this.route.navigate(['']);
      },
      error:(err)=>{
      }
    })
  }
}
