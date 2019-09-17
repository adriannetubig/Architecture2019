import { Component } from '@angular/core';

import { UsersService } from '../../services/users.service';

import { User } from '../../models/user';
import { RequestResult } from '../../models/base/request-result';
import { PageFilter } from '../../models/base/page-filter';
import { PagedList } from '../../models/base/paged-list';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  templateUrl: './users.component.html'
})
export class UsersComponent {
  requestResult: RequestResult<PagedList<User>>;
  pageFilter: PageFilter<any>;

  // users$: Observable<User[]>;

  constructor(private _usersService: UsersService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<PagedList<User>>;
    this.requestResult.model = {} as PagedList<User>;
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
    //Todo: Use this
    // this.users$ = this._usersService.read(1000, this.pageFilter).pipe(map(x=>{return x.model.items}));
    this._usersService.search(this.pageFilter).subscribe(result => {
      if (result.succeeded) {
        this.requestResult = result;
      }
    });
  }

  pageChanged(pageNumber: number) {
    this.pageFilter.pageNo = pageNumber;
    this.refresh();
  }

  delete(userId: number) {
    this._usersService.delete(userId).subscribe(result => {
      if (result.succeeded) {
        this.refresh();
      }
    });
  }
}
