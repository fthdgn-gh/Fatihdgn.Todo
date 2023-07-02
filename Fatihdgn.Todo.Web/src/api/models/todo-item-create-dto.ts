/* tslint:disable */
export interface TodoItemCreateDTO {
  content: string;
  dueAt?: string;
  isCompleted: boolean;
  listId: string;
  note: string;
  remindAt?: string;
}
