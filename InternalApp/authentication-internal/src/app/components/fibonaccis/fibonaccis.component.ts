import { Component } from '@angular/core';

import { FibonaccisService } from '../../services/fibonaccis.service';

import { Fibonacci } from '../../models/fibonacci';
import { RequestResult } from '../../models/base/request-result';
import { PageFilter } from '../../models/base/page-filter';
import { PagedList } from '../../models/base/paged-list';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  templateUrl: './fibonaccis.component.html'
})
export class FibonaccisComponent {
  requestResult: RequestResult<PagedList<Fibonacci>>;
  pageFilter: PageFilter<any>;

  constructor(private _fibonaccisService: FibonaccisService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<PagedList<Fibonacci>>;
    this.requestResult.model = {} as PagedList<Fibonacci>;
    this.requestResult.model.items = [];
    this.requestResult.model.numberOfItems = 0;
    this.requestResult.model.pageNo = 1;
    this.pageFilter = {} as PageFilter<any>;
    this.pageFilter.itemsPerPage = 10;
    this.pageFilter.pageNo = 1;
  }

  ngAfterContentInit() {
    this.refresh();
  }

  refresh() {
    this._fibonaccisService.search(this.pageFilter).subscribe(result => {
      if (result.succeeded) {
        this.requestResult = result;
      }
    });
  }

  pageChanged(pageNumber: number) {
    this.pageFilter.pageNo = pageNumber;
    this.refresh();
  }

  delete(fibonacciId: number) {
    this._fibonaccisService.delete(fibonacciId).subscribe(result => {
      if (result.succeeded) {
        this.refresh();
      }
    });
  }
}
