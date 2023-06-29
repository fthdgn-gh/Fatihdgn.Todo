/* tslint:disable */
import { ModelStateEntry } from './model-state-entry';
import { ModelValidationState } from './model-validation-state';
export interface ModelStateDictionary {
  Count: number;
  ErrorCount: number;
  HasReachedMaxErrors: boolean;
  IsValid: boolean;
  Item?: ModelStateEntry;
  Keys: Array<any>;
  MaxAllowedErrors: number;
  MaxStateDepth?: number;
  MaxValidationDepth?: number;
  Root: ModelStateEntry;
  ValidationState: ModelValidationState;
  Values: Array<any>;
}
