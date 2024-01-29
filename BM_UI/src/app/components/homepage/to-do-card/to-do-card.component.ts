import { Component, OnInit } from '@angular/core';
import { ToDoView } from 'src/app/shared/models/to-do/to-do-view.model';
import { CompanyService } from '../../company/company.service';
import { Company } from 'src/app/shared/models/company/company.model';
import { HomepageService } from '../homepage.service';

@Component({
  selector: 'app-to-do-card',
  templateUrl: './to-do-card.component.html',
  styleUrls: ['./to-do-card.component.css']
})
export class ToDoCardComponent implements OnInit {

  today:ToDoView[]=[];
  upcoming:ToDoView[]=[];
  constructor(private companyService:CompanyService,private homepageService:HomepageService) { }

  ngOnInit(): void {
    this.getTodayToDos();
    this.getUpcomingToDos();
  }

  getTodayToDos() {
    this.companyService.company$.subscribe({
      next: (company: Company | null) => {
        if (company) {
          this.homepageService.getTodayToDos(company.id).subscribe({
            next: (td) => {
              this.today = td;
            },
            error:(err)=>{
            }
          });
        }
      },
    });
  }
  getUpcomingToDos() {
    this.companyService.company$.subscribe({
      next: (company: Company | null) => {
        if (company) {
          this.homepageService.getUpcomingToDos(company.id).subscribe({
            next: (up) => {
              this.upcoming = up;
            },
            error:(err)=>{

            }
          });
        }
      },
    });
  }
}
