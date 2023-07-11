import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class NavigationService {

    private showNav$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor() { }

    getShowNav(){
        return this.showNav$.asObservable();
    }

    setShowNav(showHide: boolean) {
        this.showNav$.next(showHide);
    }

    toggleNavState() {
        this.showNav$.next(!this.showNav$.value);
    }

    isNavOpen() {
        return this.showNav$.value;
    }
}