/* tslint:disable */
import { ModelErrorCollection } from './model-error-collection';
import { ModelValidationState } from './model-validation-state';
export interface ModelStateEntry {
  attemptedValue?: string;
  errors: ModelErrorCollection;
  rawValue?: any;
  validationState: ModelValidationState;
}
