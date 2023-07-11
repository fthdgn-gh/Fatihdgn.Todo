import { ChangeDetectorRef, Component, OnDestroy, OnInit, Signal, computed } from "@angular/core";
import { LocalStorageService } from "../helpers/local-storage.service";
import { Router } from "@angular/router";
import { NavigationService } from "../helpers/navigatation.service";
import { StateManager } from "../helpers/state.manager";
import { StateService } from "../helpers/state.service";
import { SubSink } from "subsink";
import { TodoItemDto, TodoListDto, TodoTemplateDto } from "src/api/models";
import { State } from "../models/state.model";
import { toSignal } from "@angular/core/rxjs-interop";

@Component({
    selector: "todo-dashboard",
    templateUrl: "./dashboard.component.html",
    styleUrls: ["./dashboard.component.css"]
})
export class DashboardComponent implements OnInit, OnDestroy {
    private subs = new SubSink();
    public state$: Signal<State | undefined>;
    public currentList$: Signal<TodoListDto | undefined>;
    public lists$: Signal<TodoListDto[] | undefined>;
    public items$: Signal<TodoItemDto[] | undefined>;
    public templates$: Signal<TodoTemplateDto[] | undefined>;

    constructor(
        private readonly storage: LocalStorageService,
        private readonly router: Router,
        private readonly navService: NavigationService,
        public readonly state: StateService,
        private readonly stateManager: StateManager,
        private readonly cdr: ChangeDetectorRef
    ) {
        this.state$ = toSignal(this.state.value$);
        this.currentList$ = computed(() => this.state$()?.currentList);
        this.lists$ = computed(() => this.state$()?.lists);
        this.items$ = computed(() => this.state$()?.items);
        this.templates$ = computed(() => this.state$()?.templates);
    }


    ngOnInit(): void {
        this.subs.sink = this.stateManager.init().subscribe();
    }

    ngOnDestroy(): void {
        this.subs.unsubscribe();
    }

    logout() {
        this.storage.remove("login");
        this.router.navigate(["/account/login"]);
    }

    toggleSideNav() {
        this.navService.toggleNavState();
    }

    onListSelected(list: TodoListDto): void {
        this.toggleSideNav();
        this.subs.sink = this.stateManager.selectList(list).subscribe();
    }

    onItemIsCompletedChanged(item: TodoItemDto, event: Event): void {
        const isChecked = (event.target as HTMLInputElement).checked;
        this.subs.sink = this.stateManager.changeItemIsCompleted(item, isChecked).subscribe();
    }

    onItemCreated(content: string) {
        if (!content) return;
        this.subs.sink = this.stateManager.createItem(content).subscribe();
    }

    onItemDeleted(item: TodoItemDto) {
        this.subs.sink = this.stateManager.deleteItem(item).subscribe();
    }

    isItemInEditMode(item: TodoItemDto): boolean {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        return (item as any).edit === true;
    }

    switchItemToEditMode(item: TodoItemDto): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (item as any).edit = true;
    }

    saveItemChanges(item: TodoItemDto) {
        this.subs.sink = this.stateManager.updateItem(item).subscribe();
    }

    onListCreated(name: string) {
        this.toggleSideNav();
        this.subs.sink = this.stateManager.createList(name).subscribe();
    }

    isListInEditMode(list: TodoListDto): boolean {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        return (list as any).edit === true;
    }

    switchListToEditMode(item: TodoItemDto): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (item as any).edit = true;
    }

    saveListChanges(list: TodoListDto) {
        this.subs.sink = this.stateManager.updateList(list).subscribe();
    }

    onListDeleted(list: TodoListDto) {
        this.subs.sink = this.stateManager.deleteList(list).subscribe();
    }

    isTemplateInEditMode(template: TodoTemplateDto): boolean {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        return (template as any).edit === true;
    }

    switchTemplateToEditMode(template: TodoTemplateDto): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (template as any).edit = true;
    }

    onTemplateDeleted(template: TodoTemplateDto) {
        this.subs.sink = this.stateManager.deleteTemplate(template).subscribe();
    }

    saveTemplateChanges(template: TodoTemplateDto) {
        this.subs.sink = this.stateManager.updateTemplate(template).subscribe();
    }

    onCreateTemplateFromList(list: TodoListDto) {
        this.subs.sink = this.stateManager.createTemplateFromList(list).subscribe();
    }

    createListFromTemplate(template: TodoTemplateDto) {
        this.subs.sink = this.stateManager.createListFromTemplate(template).subscribe();
    }
}
