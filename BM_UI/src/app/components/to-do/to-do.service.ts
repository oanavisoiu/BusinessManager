import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToDoView } from 'src/app/shared/models/to-do/to-do-view.model';
import { ToDo } from 'src/app/shared/models/to-do/to-do.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ToDoService {

  baseApiUrl:string = environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getToDos(companyId:string){
    return this.http.get<ToDo[]>(this.baseApiUrl+'/api/todo/get-company-to-do/'+companyId);
  }
  addToDo(toDo:ToDoView){
    return this.http.post<ToDo>(this.baseApiUrl+'/api/todo/add-to-do/',toDo);
  }
  getToDo(id:string){
    return this.http.get<ToDo>(this.baseApiUrl+'/api/todo/get-to-do-by-id/'+id);
  }
  updateToDo(id:string, toDo:ToDoView){
    return this.http.put<ToDo>(this.baseApiUrl+'/api/todo/update-to-do/'+id,toDo)
  }
  deletetoDo(id:string){
    return this.http.delete<ToDo>(this.baseApiUrl+'/api/todo/delete-to-do/'+id);
  }
}
