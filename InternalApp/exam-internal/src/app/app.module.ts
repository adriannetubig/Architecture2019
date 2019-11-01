import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {NgbModule, NgbDropdown} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FibonaccisComponent } from './components/fibonaccis/fibonaccis.component';
import { FibonaccisCreateComponent } from './components/fibonaccis-create/fibonaccis-create.component';
import { FibonaccisUpdateComponent } from './components/fibonaccis-update/fibonaccis-update.component';

@NgModule({
  declarations: [
    AppComponent,
    FibonaccisComponent, FibonaccisCreateComponent, FibonaccisUpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [NgbDropdown],
  bootstrap: [AppComponent]
})
export class AppModule { }