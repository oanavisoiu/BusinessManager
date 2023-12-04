import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../components/account/account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService:AccountService){}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      setHeaders: {
        'Authorization': `Bearer ${this.accountService.getJWT()}`,
      },
    });
    return next.handle(req);
  }
}

