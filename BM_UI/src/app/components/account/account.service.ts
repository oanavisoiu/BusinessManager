import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject, map, take } from 'rxjs';
import { ConfirmEmail } from 'src/app/shared/models/account/confirm-email.model';
import { Login } from 'src/app/shared/models/account/login.model';
import { Register } from 'src/app/shared/models/account/register.model';
import { ResetPassword } from 'src/app/shared/models/account/reset-password.model';
import { User } from 'src/app/shared/models/account/user.model';
import { environment } from 'src/environments/environment';
import { CompanyService } from '../company/company.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseApiUrl = environment.baseApiUrl;
  private userSource = new ReplaySubject<User | null>(1);
  user$ = this.userSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    private companyService: CompanyService
  ) {}

  refreshUser(jwt: string | null) {
    if (jwt == null) {
      this.userSource.next(null);
      return undefined;
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http
      .get<User>(this.baseApiUrl + '/api/Account/refresh-user-token', {
        headers,
      })
      .pipe(
        map((user: User) => {
          if (user) {
            this.setUser(user);
            this.companyService.getCompany().subscribe(
              (company) => {},
              (error) => {
                console.error(error);
              }
            );
          }
        })
      );
  }

  register(model: Register) {
    return this.http.post(this.baseApiUrl + '/api/Account/register', model);
  }
  confirmEmail(model: ConfirmEmail) {
    return this.http.put(this.baseApiUrl + '/api/Account/confirm-email', model);
  }
  login(model: Login) {
    return this.http
      .post<User>(this.baseApiUrl + '/api/Account/login', model)
      .pipe(
        map((user: User) => {
          if (user) {
            this.setUser(user);
            this.companyService.getCompany().subscribe(
              (company) => {},
              (error) => {
                console.error('Error retrieving company data:', error);
              }
            );
          }
        })
      );
  }

  logout() {
    localStorage.removeItem(environment.userKey);
    sessionStorage.removeItem(environment.userKey);
    this.userSource.next(null);
    this.companyService.resetCompany();
    this.router.navigateByUrl('/account');
  }

  resendEmailConfirmationLink(email: string) {
    return this.http.post(
      this.baseApiUrl + '/api/Account/resend-email-confirmation-link/' + email,
      {}
    );
  }
  forgotUsernameorPassword(email: string) {
    return this.http.post(
      this.baseApiUrl + '/api/Account/forgot-username-or-password/' + email,
      {}
    );
  }
  resetPassword(model: ResetPassword) {
    return this.http.put(
      this.baseApiUrl + '/api/Account/reset-password',
      model
    );
  }

  getJWT() {
    const key = sessionStorage.getItem(environment.userKey);
    if (key) {
      const user: User = JSON.parse(key);
      return user.jwt;
    } else {
      return null;
    }
  }

  private setUser(user: User) {
    sessionStorage.setItem(environment.userKey, JSON.stringify(user));
    this.userSource.next(user);

    this.user$.subscribe({
      next: (response) => {
        //console.log(response);
      },
    });
  }
}
