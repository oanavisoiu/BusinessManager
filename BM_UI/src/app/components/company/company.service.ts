import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  addCompany(company: Company) {
    company.id = '00000000-0000-0000-0000-000000000000';
    return this.http
      .post<Company>(this.baseApiUrl + '/api/company/create-company', company).pipe(
        map((company: Company) => {
          if (company) {
            this.setCompany(company);
          }
        })
      );
  }

  getCompany() {
    return this.http
      .get<Company>(this.baseApiUrl + '/api/company/get-company')
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
