import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

import { User } from '../models/user';
import { RequestResult } from '../models/base/request-result';
import { PageFilter } from '../models/base/page-filter';
import { PagedList } from '../models/base/paged-list';
import { UserUpdate } from '../models/user-update';

@Injectable({
  providedIn: 'root'
})

export class UsersService {

  constructor(private _httpClient: HttpClient) {
  }

  create(user: User) {
    return this._httpClient.put<RequestResult<User>>(`${environment.url}/api/v1/Users`, user);
  }

  Read(userId: number) {
    return this._httpClient.get<RequestResult<UserUpdate>>(`${environment.url}/api/v1/Users/userId`);
  }

  update(user: User) {
    return this._httpClient.post<RequestResult<null>>(`${environment.url}/api/v1/Users`, user);
  }

  search(pageFilter: PageFilter<null>) {
    return this._httpClient.post<RequestResult<PagedList<User>>>(`${environment.url}/api/v1/Users/Search`, pageFilter);
  }

  changePassword(user: User) {
    return this._httpClient.post<RequestResult<null>>(`${environment.url}/api/v1/Users/ChangePassword`, user);
  }

  updatePassword(user: User) {
    return this._httpClient.post<RequestResult<null>>(`${environment.url}/api/v1/Users/UpdatePassword`, user);
  }

  delete(userId: Number) {
    return this._httpClient.delete<RequestResult<null>>(`${environment.url}/api/v1/Users/${userId}`);
  }
}
