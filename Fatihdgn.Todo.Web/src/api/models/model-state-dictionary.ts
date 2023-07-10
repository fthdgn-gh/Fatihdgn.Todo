/* tslint:disable */
/* eslint-disable */
import { ModelStateEntry } from './model-state-entry';
import { ModelValidationState } from './model-validation-state';
export interface ModelStateDictionary {
  Count?: number;
  ErrorCount?: number;
  HasReachedMaxErrors?: boolean;
  IsValid?: boolean;
  Item?: null | ModelStateEntry;
  Keys?: Array<any>;
  MaxAllowedErrors?: number;
  MaxStateDepth?: null | number;
  MaxValidationDepth?: null | number;
  Root?: ModelStateEntry;
  ValidationState?: ModelValidationState;
  Values?: Array<any>;
}
