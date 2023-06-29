/* tslint:disable */
import { Exception } from './exception';
export interface ModelError {
  errorMessage: string;
  exception?: Exception;
}
