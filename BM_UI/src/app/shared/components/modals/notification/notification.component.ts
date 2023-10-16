import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {

  @Input() isSuccess:boolean=true;
  @Input() title:string='';
  @Input() message:string='';

  constructor(private modal:NgbModal) { }

  ngOnInit(): void {
  }

  closeNotification()
  {
    this.modal.dismissAll();
  }

}
