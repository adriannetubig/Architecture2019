import { Component } from '@angular/core';
import { Router } from "@angular/router"

import { UsersService } from '../../services/users.service';

import { RequestResult } from '../../models/base/request-result';
import { User } from '../../models/user';
import { UserUpdate } from '../../models/user-update';

@Component({
  templateUrl: './users-change-password.component.html'
})

export class UsersChangePasswordComponent {
  requestResult: RequestResult<UserUpdate>;
  user: User;

  constructor(private _router: Router, private _usersService: UsersService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<UserUpdate>;
    this.requestResult.model = {} as UserUpdate;
    this.user = {} as User;
  }

  ngAfterContentInit() {
  }

  submit() {
    this._usersService.changePassword(this.user).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this._router.navigate([`/users`]);
      }
    });
  }
}
