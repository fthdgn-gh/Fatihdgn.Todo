import { Injectable } from "@angular/core";
import { Observable, firstValueFrom, of, switchMap } from "rxjs";
import { ItemsService, ListsService, TemplatesService } from "src/api/services";
import { StateService } from "./state.service";

@Injectable({
  providedIn: 'root'
})
export class StateManager {
  constructor(
    private readonly lists: ListsService,
    private readonly items: ItemsService,
    private readonly templates: TemplatesService,
    private readonly state: StateService
  ) {

  }
  async initAsync(): Promise<void> {
    const state = this.state.value;
    state.lists = await firstValueFrom(this.lists.getAllLists());
    if (state.lists.length > 0) {
      state.items = await firstValueFrom(this.items.getAllItemsByListId({ id: state.lists[0].id! }));
    }
    state.templates = await firstValueFrom(this.templates.getAllTemplates());
    this.state.value = state;
  }
}