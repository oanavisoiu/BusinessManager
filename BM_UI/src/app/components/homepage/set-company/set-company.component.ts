import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AddCompanyComponent } from '../../company/add-company/add-company.component';
import { CompanyService } from '../../company/company.service';

@Component({
  selector: 'app-set-company',
  templateUrl: './set-company.component.html',
  styleUrls: ['./set-company.component.css']
})
export class SetCompanyComponent implements OnInit {

  constructor(
    private modalService: NgbModal,
    public companyService: CompanyService) { }

  ngOnInit(): void {
  }

  addCompanyClick() {
    const modalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
    };
    const modalRef = this.modalService.open(AddCompanyComponent, modalOptions);
  }
}
