import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  constructor(private modalService:NgbModal) { }

  ngOnInit(): void {
  }

  onRegister(){
    const modalRef = this.modalService.open(RegisterComponent);
  }

}
