import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationsComponent } from './components/authentications/authentications.component';
import { FibonaccisComponent } from './components/fibonaccis/fibonaccis.component';
import { FibonaccisCreateComponent } from './components/fibonaccis-create/fibonaccis-create.component';
import { FibonaccisUpdateComponent } from './components/fibonaccis-update/fibonaccis-update.component';
import { UsersComponent } from './components/users/users.component';
import { UsersCreateComponent } from './components/users-create/users-create.component';
import { UsersChangePasswordComponent } from './components/users-change-password/users-change-password.component';
import { UsersUpdateComponent } from './components/users-update/users-update.component';
import { UsersUpdatePasswordComponent } from './components/users-update-password/users-update-password.component';

const routes: Routes = [
  { path: 'authentications', component: AuthenticationsComponent, pathMatch: 'full' },

  { path: 'fibonaccis', component: FibonaccisComponent, pathMatch: 'full' },
  { path: 'fibonaccis/create', component: FibonaccisCreateComponent },
  { path: 'fibonaccis/update/:fibonacciId', component: FibonaccisUpdateComponent },

  { path: 'users', component: UsersComponent, pathMatch: 'full' },
  { path: 'users/create', component: UsersCreateComponent },
  { path: 'users/change-password', component: UsersChangePasswordComponent },
  { path: 'users/update/:userId', component: UsersUpdateComponent },
  { path: 'users/update-password/:userId', component: UsersUpdatePasswordComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
