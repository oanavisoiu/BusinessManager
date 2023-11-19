import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map } from 'rxjs';
import { Company } from 'src/app/shared/models/company/company.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  baseApiUrl = environment.baseApiUrl;
  private companySubject = new BehaviorSubject<Company | null>(null);
  company$ = this.companySubject.asObservable();

  constructor(private http: HttpClient) {}

  addCompany(company: Company, jwt: string | null) {
    company.id = '00000000-0000-0000-0000-000000000000';
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http
      .post<Company>(this.baseApiUrl + '/api/company/create-company', company, {
        headers,
      }).pipe(
        map((company: Company) => {
          if (company) {
            this.setCompany(company);
          }
        })
      );
  }

  getCompany(jwt: string | null) {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http
      .get<Company>(this.baseApiUrl + '/api/company/get-company', { headers })
      .pipe(
        map((company: Company) => {
          if (company) {
            this.setCompany(company);
          }
        })
      );
  }
  resetCompany() {
    this.companySubject.next(null);
  }

  private setCompany(company: Company) {
    this.companySubject.next(company);
    localStorage.setItem(environment.userKey, JSON.stringify(company));
  }
}
