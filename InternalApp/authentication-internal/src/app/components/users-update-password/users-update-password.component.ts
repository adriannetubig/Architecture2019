import { Component } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router"

import { UsersService } from '../../services/users.service';

import { RequestResult } from '../../models/base/request-result';
import { Role } from 'src/app/models/role';
import { User } from '../../models/user';
import { UserUpdate } from '../../models/user-update';

@Component({
  templateUrl: './users-update-password.component.html'
})

export class UsersUpdatePasswordComponent {
  requestResult: RequestResult<UserUpdate>;
  user: User;
  roles: Role[];
  private _userId: number;

  constructor(private _activatedRoute: ActivatedRoute, private _router: Router, private _usersService: UsersService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<UserUpdate>;
    this.requestResult.model = {} as UserUpdate;
    this.user = {} as User;
    this.roles = {} as Role[];
  }

  ngAfterContentInit() {
    this._userId = this._activatedRoute.snapshot.params.userId;
    this.refresh();
  }

  submit() {
    this._usersService.updatePassword(this.user).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this._router.navigate([`/users`]);
      }
    });
  }

  private refresh() {
    this._usersService.Read(this._userId).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this.user = result.model.user;
        this.roles = result.model.roles;
      }
    });
  }
}
