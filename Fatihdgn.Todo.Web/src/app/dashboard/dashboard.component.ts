import { Component, OnDestroy, OnInit, Signal, computed } from "@angular/core";
import { LocalStorageService } from "../helpers/local-storage.service";
import { Router } from "@angular/router";
import { NavigationService } from "../helpers/navigatation.service";
import { StateManager } from "../helpers/state.manager";
import { StateService } from "../helpers/state.service";
import { SubSink } from "subsink";
import { TodoItemDto, TodoListDto } from "src/api/models";
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

    constructor(
        private readonly storage: LocalStorageService,
        private readonly router: Router,
        private readonly navService: NavigationService,
        public readonly state: StateService,
        private readonly stateManager: StateManager
    ) {
        this.state$ = toSignal(this.state.value$);
        this.currentList$ = computed(() => this.state$()?.currentList);
        this.lists$ = computed(() => this.state$()?.lists);
        this.items$ = computed(() => this.state$()?.items);
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
        this.navService.setShowNav(true);
    }

    selectList(list: TodoListDto): void {
        this.subs.sink = this.stateManager.selectList(list).subscribe();
    }
}
