import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationsComponent } from './components/authentications/authentications.component';
import { UsersComponent } from './components/users/users.component';
import { UsersCreateComponent } from './components/users-create/users-create.component';
import { UsersUpdateComponent } from './components/users-update/users-update.component';

const routes: Routes = [
  { path: 'authentications', component: AuthenticationsComponent, pathMatch: 'full' },

  { path: 'users', component: UsersComponent, pathMatch: 'full' },
  { path: 'users/create', component: UsersCreateComponent },
  { path: 'users/update/:userId', component: UsersUpdateComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
