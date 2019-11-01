import { Component } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router"

import { FibonaccisService } from '../../services/fibonaccis.service';

import { RequestResult } from '../../models/base/request-result';
import { Fibonacci } from '../../models/fibonacci';

@Component({
  templateUrl: './fibonaccis-update.component.html'
})

export class FibonaccisUpdateComponent {
  requestResult: RequestResult<Fibonacci>;
  fibonacci: Fibonacci;
  private _fibonacciId: number;

  constructor(private _activatedRoute: ActivatedRoute, private _router: Router, private _fibonaccisService: FibonaccisService) { }

  ngOnInit() {
    this.requestResult = {} as RequestResult<Fibonacci>;
    this.requestResult.model = {} as Fibonacci;
    this.fibonacci = {} as Fibonacci;
  }

  ngAfterContentInit() {
    this._fibonacciId = this._activatedRoute.snapshot.params.fibonacciId;
    this.refresh();
  }

  submit() {
    this._fibonaccisService.update(this.fibonacci).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this._router.navigate([`/fibonaccis`]);
      }
    });
  }

  private refresh() {
    this._fibonaccisService.read(this._fibonacciId).subscribe(result => {
      this.requestResult = result;
      if (result.succeeded) {
        this.fibonacci = result.model;
      }
    });
  }
}
