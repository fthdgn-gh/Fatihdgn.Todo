import { TodoItemDto, TodoListDto, TodoTemplateDto } from "src/api/models";

export interface State {
  lists: Array<TodoListDto>;
  items: Array<TodoItemDto>;
  templates: Array<TodoTemplateDto>;
  currentList: TodoListDto;
  currentItem: TodoItemDto;
  currentTemplate: TodoTemplateDto;
}