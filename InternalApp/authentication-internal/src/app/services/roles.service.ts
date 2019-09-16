import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

import { Role } from '../models/role';
import { RequestResult } from '../models/base/request-result';

@Injectable({
  providedIn: 'root'
})

export class RolesService {

  constructor(private _httpClient: HttpClient) {
  }

  Read() {
    return this._httpClient.get<RequestResult<Role[]>>(`${environment.url}/api/v1/Roles`);
  }
}
