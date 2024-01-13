import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {

  @Input() cardTitle: string='';
  @Input() items: any;

  constructor() { }

  ngOnInit(): void {
  }

  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

}
