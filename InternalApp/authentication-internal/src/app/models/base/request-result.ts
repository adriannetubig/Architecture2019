import { Exception } from './exception';

export interface RequestResult<T> {
  succeeded: boolean;
  requestDateTime: Date;
  resultDateTime: Date;
  model: T;

  exceptions: Exception[];
  errors: string[];
  messages: string[];
  warnings: string[];
}
