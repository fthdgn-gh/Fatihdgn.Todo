import { Injectable } from "@angular/core";
import { Observable, of, switchMap, tap } from "rxjs";
import { ItemsService, ListsService, TemplatesService } from "src/api/services";
import { StateService } from "./state.service";
import { TodoItemDto, TodoListDto, TodoTemplateDto } from "src/api/models";
import { LocalStorageService } from "./local-storage.service";

@Injectable({
    providedIn: "root"
})
export class StateManager {

    constructor(
        private readonly lists: ListsService,
        private readonly items: ItemsService,
        private readonly templates: TemplatesService,
        private readonly state: StateService,
        private readonly storage: LocalStorageService
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
                    let list: TodoListDto | null = null;
                    const lastSelectedListId = this.storage.get<string>("lastSelectedListId");
                    if (lastSelectedListId) {
                        list = lists.filter(x => x.id == lastSelectedListId)[0];
                    }
                    if (!list)
                        list = lists[0];
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
        this.storage.set("lastSelectedListId", list.id);
        return this.items.getAllItemsByListId({ id: list.id }).pipe(
            tap(items => {
                this.state.update(state => state.items = items);
            }),
            switchMap(() => of())
        );
    }

    changeItemIsCompleted(item: TodoItemDto, isCompleted: boolean): Observable<never> {
        if (!item.id) return of();
        this.state.update(state => state.currentItem = item);
        return this.items.patchItem({ id: item.id, body: { isCompleted } }).pipe(
            switchMap(() => of())
        );
    }

    createItem(content: string): Observable<never> {
        const currentList = this.state.value.currentList;
        if (!currentList.id) return of();

        return this.items.createItem({
            body: {
                listId: currentList.id,
                content,
            }
        }).pipe(
            tap(item => {
                this.state.update(state => {
                    state.items.push(item);
                });
            }),
            switchMap(() => of())
        );
    }


    updateItem(item: TodoItemDto) {
        if (!item.id) return of();
        this.state.update(state => state.currentItem = item);

        return this.items.updateItem({
            id: item.id,
            body: { ...item }
        }).pipe(
            tap(updatedItem => {
                this.state.update(state => {
                    state.items.splice(state.items.indexOf(item), 1, updatedItem);
                });
            })
        );
    }

    deleteItem(item: TodoItemDto) {
        if (!item.id) return of();
        const currentItem = this.state.value.currentItem;
        if (item.id == currentItem.id) this.state.update(state => state.currentItem = {});

        return this.items.removeItem({ id: item.id }).pipe(
            tap(() => {
                this.state.update(state => {
                    state.items.splice(state.items.indexOf(item), 1);
                });
            }),
            switchMap(() => of())
        );
    }

    createList(name: string): Observable<never> {
        return this.lists.createList({
            body: { name }
        }).pipe(
            tap(list => {
                this.state.update(state => {
                    state.lists.push(list);
                    state.currentList = list;
                });
            }),
            switchMap(list => this.selectList(list)),
            switchMap(() => of())
        );
    }

    createListFromTemplate(template: TodoTemplateDto): Observable<never> {
        if (!template.id) return of();
        return this.lists.createListByTemplate({
            id: template.id
        }).pipe(
            tap(list => {
                this.state.update(state => {
                    state.lists.push(list);
                    state.currentList = list;
                });
            }),
            switchMap(list => this.selectList(list)),
            switchMap(() => of())
        );
    }

    updateList(list: TodoListDto) {
        if (!list.id) return of();
        this.state.update(state => state.currentList = list);

        return this.lists.updateList({
            id: list.id,
            body: { ...list }
        }).pipe(
            tap(updatedList => {
                this.state.update(state => {
                    state.lists.splice(state.lists.indexOf(list), 1, updatedList);
                });
            })
        );
    }

    deleteList(list: TodoListDto) {
        if (!list.id) return of();
        const currentList = this.state.value.currentList;

        return this.lists.removeList({ id: list.id }).pipe(
            tap(() => {
                this.state.update(state => {
                    state.lists.splice(state.lists.indexOf(list), 1);
                });
            }),
            switchMap(() => {
                if (list.id == currentList.id) {
                    const state = this.state.value;
                    this.state.update(state => state.currentList = {});
                    if (state.lists.length > 0)
                        return this.selectList(state.lists[0]);
                    else
                        this.state.update(state => {
                            while (state.items.length > 0)
                                state.items.pop();
                        });
                }
                return of();
            }),
            switchMap(() => of())
        );
    }

    createTemplateFromList(list: TodoListDto): Observable<never> {
        if (!list.id) return of();
        return this.templates.createTemplateByList({ id: list.id }).pipe(
            tap(item => {
                this.state.update(state => {
                    state.templates.push(item);
                });
            }),
            switchMap(() => of())
        );
    }


    updateTemplate(template: TodoTemplateDto) {
        if (!template.id) return of();
        this.state.update(state => state.currentTemplate = template);

        return this.templates.updateTemplate({
            id: template.id,
            body: { ...template }
        }).pipe(
            tap(updatedTemplate => {
                this.state.update(state => {
                    state.templates.splice(state.items.indexOf(template), 1, updatedTemplate);
                });
            })
        );
    }

    deleteTemplate(template: TodoTemplateDto) {
        if (!template.id) return of();
        const currentTemplate = this.state.value.currentTemplate;
        if (template.id == currentTemplate.id) this.state.update(state => state.currentTemplate = {});

        return this.templates.removeTemplate({ id: template.id }).pipe(
            tap(() => {
                this.state.update(state => {
                    state.templates.splice(state.items.indexOf(template), 1);
                });
            }),
            switchMap(() => of())
        );
    }
}