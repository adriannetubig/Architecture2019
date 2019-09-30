import { Component } from '@angular/core';
import { Router } from "@angular/router"
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { FibonaccisService } from '../../services/fibonaccis.service';

import { RequestResult } from '../../models/base/request-result';
import { Fibonacci } from '../../models/fibonacci';

@Component({
  templateUrl: './fibonaccis-create.component.html'
})
export class FibonaccisCreateComponent {
  requestResult: RequestResult<Fibonacci>;
  fibonacci: Fibonacci;

  constructor(private _router: Router, private _fibonaccisService: FibonaccisService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<Fibonacci>;
    this.requestResult.model = {} as Fibonacci;
    this.fibonacci = {} as Fibonacci;
  }

  submit() {
    this._fibonaccisService.create(this.fibonacci).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this._router.navigate([`/fibonaccis`]);
      }
    });
  }

}
