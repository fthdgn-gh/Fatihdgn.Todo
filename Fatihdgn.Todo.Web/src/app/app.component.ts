import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { TodoItemCreateDTO, TodoItemDTO } from 'src/api/models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {

  constructor() {
    this.items.set([
      {
        isCompleted: false,
        content: 'not completed',
        note: '',
      },
      {
        isCompleted: true,
        content: 'completed',
        note: '',
      },
    ]);
  }
  title = 'Todo List';
  items = signal(new Array<TodoItemCreateDTO>());
  text = signal('');

  changeText(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.text.set(value);
  }

  addItem() {
    this.items.mutate((_items) => {
      const todoItem: TodoItemCreateDTO = {
        content: this.text(),
        isCompleted: false,
        note: '',
      };
      _items.push(todoItem);
      this.text.set('');
    });
  }

  markAsCompleted(item: TodoItemCreateDTO) {
    item.isCompleted = true;
  }

  markAsNew(item: TodoItemCreateDTO) {
    item.isCompleted = false;
  }

  removeItem(item: TodoItemCreateDTO) {
    this.items.mutate(items => {
      return items.splice(items.indexOf(item), 1);
    });
  }
}
