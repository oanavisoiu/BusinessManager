import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/shared/models/login.model';
import { Register } from 'src/app/shared/models/register.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseApiUrl=environment.baseApiUrl;

  constructor(private http:HttpClient) { }

  register(model:Register){
    return this.http.post(this.baseApiUrl+'/api/Account/register',model);
  }

  login(model:Login){
    return this.http.post(this.baseApiUrl+'/api/Account/login',model);
  }
}
