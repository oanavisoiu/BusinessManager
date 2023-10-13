import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  constructor(private modalService:NgbModal) { }

  ngOnInit(): void {
  }
  onLogin(){
    const modalRef = this.modalService.open(LoginComponent);
  }
  onRegister(){
    const modalRef = this.modalService.open(RegisterComponent);
  }

}
