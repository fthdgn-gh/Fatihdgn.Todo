import { Injectable } from "@angular/core";
import { Observable, of, switchMap, tap } from "rxjs";
import { ItemsService, ListsService, TemplatesService } from "src/api/services";
import { StateService } from "./state.service";
import { TodoItemDto, TodoListDto } from "src/api/models";

@Injectable({
    providedIn: "root"
})
export class StateManager {

    constructor(
        private readonly lists: ListsService,
        private readonly items: ItemsService,
        private readonly templates: TemplatesService,
        private readonly state: StateService
    ) {

    }
    init(): Observable<never> {
        return this.lists.getAllLists().pipe(
            tap(lists => {
                this.state.update(state => state.lists = lists);
            }),
            switchMap(lists => {
                const state = this.state.value;
                if (lists.length > 0) {
                    const list = lists[0];
                    state.currentList = list;
                    return this.items.getAllItemsByListId({ id: list.id! });
                }
                this.state.value = state;
                return of([]);
            }),
            tap(items => {
                this.state.update(state => state.items = items);
            }),
            switchMap(() => this.templates.getAllTemplates()),
            tap(templates => {
                this.state.update(state => state.templates = templates);
            }),
            switchMap(() => of())
        );
    }

    selectList(list: TodoListDto): Observable<never> {
        if (!list.id) return of();

        this.state.update(state => state.currentList = list);
        return this.items.getAllItemsByListId({ id: list.id }).pipe(
            tap(items => {
                this.state.update(state => state.items = items);
            }),
            switchMap(() => of())
        );
    }

    itemIsCompletedChanged(item: TodoItemDto, isCompleted: boolean): Observable<never> {
        if (!item.id) return of();
        this.state.update(state => state.currentItem = item);
        return this.items.patchItem({ id: item.id, body: { isCompleted } }).pipe(
            switchMap(this.selectList),
            switchMap(() => of())
        );
    }
}