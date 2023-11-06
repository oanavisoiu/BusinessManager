import { Injectable } from '@angular/core';
import { NotificationComponent } from './components/modals/notification/notification.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private modalService:NgbModal) { }

  showNotification(isSuccess: boolean, title: string, message: string) {
    const modalRef = this.modalService.open(NotificationComponent);
    modalRef.componentInstance.isSuccess = isSuccess;
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.message = message;
  }
}
