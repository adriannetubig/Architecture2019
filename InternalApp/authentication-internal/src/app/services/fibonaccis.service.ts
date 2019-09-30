import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

import { Fibonacci } from '../models/fibonacci';
import { RequestResult } from '../models/base/request-result';
import { PageFilter } from '../models/base/page-filter';
import { PagedList } from '../models/base/paged-list';

@Injectable({
  providedIn: 'root'
})

export class FibonaccisService {

  constructor(private _httpClient: HttpClient) {
  }

  create(fibonacci: Fibonacci) {
    return this._httpClient.put<RequestResult<Fibonacci>>(`${environment.url}/api/v1/Fibonaccis`, fibonacci);
  }

  Read(fibonacciId: number) {
    return this._httpClient.get<RequestResult<Fibonacci>>(`${environment.url}/api/v1/Fibonaccis/${fibonacciId}`);
  }

  update(fibonacci: Fibonacci) {
    return this._httpClient.post<RequestResult<null>>(`${environment.url}/api/v1/Fibonaccis`, fibonacci);
  }

  search(pageFilter: PageFilter<null>) {
    return this._httpClient.post<RequestResult<PagedList<Fibonacci>>>(`${environment.url}/api/v1/Fibonaccis/Search`, pageFilter);
  }

  delete(fibonacciId: Number) {
    return this._httpClient.delete<RequestResult<null>>(`${environment.url}/api/v1/Fibonaccis/${fibonacciId}`);
  }
}
