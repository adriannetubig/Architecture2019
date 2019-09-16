import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';

import { environment } from '../../environments/environment';

import { Authentication } from '../models/base/authentication';
import { User } from '../models/user';
import { RequestResult } from '../models/base/request-result';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationsService {

  constructor(private _httpClient: HttpClient, _httpBackend: HttpBackend) {
    this._httpClient = new HttpClient(_httpBackend);
  }

  login(user: User) {
    return this._httpClient.post<RequestResult<Authentication>>(`${environment.url}/api/v1/Authentications`, user);
  }

}
