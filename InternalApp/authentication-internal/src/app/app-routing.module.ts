import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationsComponent } from './components/authentications/authentications.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  { path: 'authentications', component: AuthenticationsComponent, pathMatch: 'full' },
  
  { path: 'users', component: UsersComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
