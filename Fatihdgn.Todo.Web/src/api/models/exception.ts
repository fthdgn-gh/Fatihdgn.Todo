/* tslint:disable */
export interface Exception {
  InnerException?: Exception;
  Message: string;
  Source?: string;
  StackTrace?: string;
}
