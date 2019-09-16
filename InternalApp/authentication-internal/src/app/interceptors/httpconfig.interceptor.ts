import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { Authentication } from '../models/base/authentication';

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      if (!request.url.includes(`Authentications`)) {

        var authenticationString: string;
        var authentication = {} as Authentication;

        const value = "; " + document.cookie;
        const parts = value.split("; " + 'UserInternalAuthentication' + "=");

        if (parts.length == 2) {
            authenticationString = parts.pop().split(";").shift();
        }

        if (authenticationString)
          authentication = JSON.parse(authenticationString);
        else
          window.open(environment.loginUrl, `_self`);

        if (authentication.token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + authentication.token) });
        }

        if (!request.headers.has('Content-Type')) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }

        request = request.clone({ headers: request.headers.set('Accept', 'application/json') });

        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                }
                return event;
            }),
            catchError((error: HttpErrorResponse) => {
                let data = {};
                data = {
                    reason: error && error.error && error.error.reason ? error.error.reason : '',
                    status: error.status
                };
                console.log(data)
                return throwError(error);
            }));
        }
    }
}
