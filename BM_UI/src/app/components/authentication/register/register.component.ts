import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private router:Router,private modal:NgbModal) { }

  ngOnInit(): void {
  }

  onClose(){
    this.modal.dismissAll();
    this.router.navigate(['/authentication']);
  }

}
