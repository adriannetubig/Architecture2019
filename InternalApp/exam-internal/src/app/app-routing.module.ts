import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FibonaccisComponent } from './components/fibonaccis/fibonaccis.component';
import { FibonaccisCreateComponent } from './components/fibonaccis-create/fibonaccis-create.component';
import { FibonaccisUpdateComponent } from './components/fibonaccis-update/fibonaccis-update.component';

const routes: Routes = [
  { path: 'fibonaccis', component: FibonaccisComponent, pathMatch: 'full' },
  { path: 'fibonaccis/create', component: FibonaccisCreateComponent },
  { path: 'fibonaccis/update/:fibonacciId', component: FibonaccisUpdateComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
