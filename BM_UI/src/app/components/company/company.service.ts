import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, switchMap } from 'rxjs';
import { Company } from 'src/app/shared/models/company/company.model';
import { UpdateCompany } from 'src/app/shared/models/company/update-company.model';
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
  updateCompany(updateCompany: UpdateCompany) {
    return this.http.put<UpdateCompany>(this.baseApiUrl + '/api/company/update-company', updateCompany).pipe(
      switchMap(() => this.getCompany())
    );
  }
  deleteCompany() {
    return this.http.delete<Company>(this.baseApiUrl + '/api/company/delete-company').pipe(
      map((company: Company) => {
        if (company) {
          this.resetCompany();
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
