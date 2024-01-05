import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CompanyService } from '../../company/company.service';
import { ToDoService } from '../to-do.service';
import CustomStore from 'devextreme/data/custom_store';
import { catchError, of } from 'rxjs';
import { ToDo } from 'src/app/shared/models/to-do/to-do.model';
import { ToDoView } from 'src/app/shared/models/to-do/to-do-view.model';

@Component({
  selector: 'app-to-do-calendar',
  templateUrl: './to-do-calendar.component.html',
  styleUrls: ['./to-do-calendar.component.css']
})
export class ToDoCalendarComponent implements OnInit {

  currentDate:any;
  toDos:any;
  constructor(private companyService:CompanyService,private toDoService:ToDoService) { }

  ngOnInit(): void {
    this.currentDate=new Date();
    this.operations();

  }
  operations() {
    this.companyService.company$.subscribe((company) => {
      if (company) {
        const dataService = this.toDoService;
        this.toDos = new CustomStore({
          key: 'id',
          load: () => {
            return dataService
              .getToDos(company.id)
              .pipe(
                catchError((error) => {
                  console.error('Error loading to dos:', error);
                  return of([]);
                })
              )
              .toPromise();
          },
          insert: (values: any) => {
            return new Promise<ToDo>((resolve, reject) => {
              const newToDo: ToDo = { ...values };
              console.log(values);
              newToDo.companyId = company.id;
              if(values.description){
                newToDo.description=values.description;
              }
              else{
                newToDo.description="";
              }
              if(values.recurrenceRule){
                newToDo.recurrenceRule=values.recurrenceRule;
              }
              else{
                newToDo.recurrenceRule="";
              }
              if(values.allDay){
                newToDo.allDay=values.allDay;
              }
              else{
                newToDo.allDay=false;
              }
              console.log(newToDo);
              dataService.addToDo(newToDo).subscribe({
                next: (toDo:ToDo) => {
                  resolve(toDo);
                },
                error: (empError) => {
                  reject(empError);
                },
              });
            });
          },
          update: (key, values) => {
            const toDoId = key;
            return new Promise((resolve, reject) => {
              dataService.getToDo(toDoId).subscribe({
                next: (toDo) => {
                  Object.assign(toDo, values);
                  const { id, ...updatedValues } = toDo;
                  const updatedToDo=updatedValues;
                  dataService.updateToDo(toDoId, updatedToDo).subscribe({
                    next: (result) => {
                      resolve(toDo);
                    },
                    error: (error) => {
                      reject(error);
                    }
                  });
                },
                error: (error) => {
                  reject(error);
                }
              });
            });
          },
          remove: (key) => {
            return new Promise<void>((resolve, reject) => {
              dataService.deletetoDo(key).subscribe({
                next: () => {
                  resolve();
                },
                error: (err) => {
                  reject(err);
                },
              });
            });
          },
        });
      }
    });
  }
}
