import { Injectable } from "@angular/core";
import { Observable, of, switchMap, tap } from "rxjs";
import { ItemsService, ListsService, TemplatesService } from "src/api/services";
import { StateService } from "./state.service";
import { TodoListDto } from "src/api/models";

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
                const state = this.state.value;
                state.lists = lists;
                this.state.value = state;
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
                const state = this.state.value;
                state.items = items;
                this.state.value = state;
            }),
            switchMap(() => this.templates.getAllTemplates()),
            tap(templates => {
                const state = this.state.value;
                state.templates = templates;
                this.state.value = state;
            }),
            switchMap(() => of())
        );
    }

    selectList(list: TodoListDto): Observable<never> {
        if (!list.id) return of();

        const state = this.state.value;
        state.currentList = list;
        this.state.value = state;
        return this.items.getAllItemsByListId({ id: list.id }).pipe(
            tap(items => {
                const state = this.state.value;
                state.items = items;
                this.state.value = state;
            }),
            switchMap(() => of())
        );
    }
}