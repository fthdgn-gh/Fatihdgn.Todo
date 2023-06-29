import { Component, signal } from '@angular/core';
import { TodoItemCreateDTO, TodoItemDTO } from 'src/api/models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Fatihdgn.Todo.Web';
  items = signal(new Array<TodoItemCreateDTO>());
  text = signal("");

  changeText(event: Event){
    const value = (event.target as HTMLInputElement).value;
    this.text.set(value);
  }

  addItem() {
    this.items.mutate(_items => {
      const todoItem: TodoItemCreateDTO = {
        content: this.text(),
        isCompleted: false,
        note: ""
      };
      _items.push(todoItem);
      this.text.set("");
    })
  }
}
