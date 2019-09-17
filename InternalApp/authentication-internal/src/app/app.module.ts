import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {NgbModule, NgbDropdown} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpConfigInterceptor} from './interceptors/httpconfig.interceptor';

import { AuthenticationsComponent } from './components/authentications/authentications.component';
import { UsersComponent } from './components/users/users.component';
import { UsersCreateComponent } from './components/users-create/users-create.component';
import { UsersChangePasswordComponent } from './components/users-change-password/users-change-password.component';
import { UsersUpdateComponent } from './components/users-update/users-update.component';
import { UsersUpdatePasswordComponent } from './components/users-update-password/users-update-password.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthenticationsComponent,
    UsersComponent, UsersCreateComponent, UsersChangePasswordComponent, UsersUpdateComponent, UsersUpdatePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [NgbDropdown,
    { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
