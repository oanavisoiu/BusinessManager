import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/shared/models/employee/employee.model';

@Component({
  selector: 'app-to-do-card',
  templateUrl: './to-do-card.component.html',
  styleUrls: ['./to-do-card.component.css']
})
export class ToDoCardComponent implements OnInit {

  todos:Employee[]=[];
  constructor() { }

  ngOnInit(): void {
  }

}
