/* tslint:disable */
/* eslint-disable */
import { ModelErrorCollection } from './model-error-collection';
import { ModelValidationState } from './model-validation-state';
export interface ModelStateEntry {
  attemptedValue?: null | string;
  errors?: ModelErrorCollection;
  rawValue?: null | any;
  validationState?: ModelValidationState;
}
