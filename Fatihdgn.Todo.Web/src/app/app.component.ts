import { ChangeDetectionStrategy, Component, computed, signal } from '@angular/core';
import { TodoItemCreateDto } from 'src/api/models';

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
  items = signal(new Array<TodoItemCreateDto>());
  shownItems = computed(() => {
    return [...this.items()].reverse();
  })
  text = signal('');

  changeText(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.text.set(value);
  }

  addItem() {
    this.items.mutate((_items) => {
      const todoItem: TodoItemCreateDto = {
        content: this.text(),
        isCompleted: false,
        note: '',
      };
      _items.push(todoItem);
      this.text.set('');
    });
  }

  markAsCompleted(item: TodoItemCreateDto) {
    item.isCompleted = true;
  }

  markAsNew(item: TodoItemCreateDto) {
    item.isCompleted = false;
  }

  removeItem(item: TodoItemCreateDto) {
    this.items.mutate(items => {
      return items.splice(items.indexOf(item), 1);
    });
  }
}
