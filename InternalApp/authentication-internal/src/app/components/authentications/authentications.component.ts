import { Component } from '@angular/core';
import { Location } from '@angular/common';

import { AuthenticationsService } from '../../services/authentications.service';

import { Authentication } from '../../models/base/authentication';
import { User } from '../../models/user';
import { RequestResult } from '../../models/base/request-result';

@Component({
  templateUrl: './authentications.component.html'
})
export class AuthenticationsComponent {
  requestResult: RequestResult<Authentication>;
  user: User;

  constructor(private _location: Location, private _authenticationsService: AuthenticationsService) { }

  ngOnInit() {
    this.user = {} as User;
  }

  login() {
    this._authenticationsService.login(this.user).subscribe(result =>
      {
        this.requestResult = result;
        if (this.requestResult.succeeded) {

          const date = new Date(this.requestResult.model.expiration);
          const value = JSON.stringify(result.model);
          
          document.cookie = `UserInternalAuthentication=${value}; expires=${date};path=/`;
          this._location.back();
        }
        else {
          console.error(this.requestResult);
        }
      });

  }
}
