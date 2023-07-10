import { Injectable } from "@angular/core";
import { toSignal } from '@angular/core/rxjs-interop';
import { BehaviorSubject } from "rxjs";
import { State } from "../models/state.model";

@Injectable()
export class StateService {
  private _state$: BehaviorSubject<State> = new BehaviorSubject<State>({
    items: [],
    lists: [],
    templates: [],
    currentItem: {},
    currentList: {},
    currentTemplate: {}
  });

  public get state$() {
    return toSignal(this._state$.asObservable());
  }

  public set state(value: State) {
    this._state$.next(value);
  }

  public get state(): State {
    return this._state$.getValue();
  }
}