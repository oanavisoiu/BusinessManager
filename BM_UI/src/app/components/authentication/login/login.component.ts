import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router:Router,private modal:NgbModal) { }

  ngOnInit(): void {

  }
  onClose(){
    this.modal.dismissAll();
    this.router.navigate(['/authentication']);
  }


}
