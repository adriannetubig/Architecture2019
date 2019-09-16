import { Component } from '@angular/core';
import { Router } from "@angular/router"
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { RolesService } from '../../services/roles.service';
import { UsersService } from '../../services/users.service';

import { RequestResult } from '../../models/base/request-result';
import { Role } from 'src/app/models/role';
import { User } from '../../models/user';

@Component({
  templateUrl: './users-create.component.html'
})
export class UsersCreateComponent {
  requestResult: RequestResult<User>;
  user: User;
  roles$: Observable<Role[]>;

  constructor(private _router: Router, private _rolesService: RolesService, private _usersService: UsersService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<User>;
    this.requestResult.model = {} as User;
    this.user = {} as User;
    this.roles$ = {} as Observable<Role[]>;
  }

  ngAfterContentInit() {
    this.refreshRoles();
  }

  submit() {
    this._usersService.create(this.user).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this._router.navigate([`/users`]);
      }
    });
  }

  private refreshRoles() {
    this.roles$ = this._rolesService.Read().pipe(map(x=>{return x.model}));
  }
}
