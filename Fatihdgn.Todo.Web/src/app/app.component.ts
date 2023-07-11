import { Component, OnInit } from "@angular/core";
import { LocalStorageService } from "./helpers/local-storage.service";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
    constructor(
        private readonly storage: LocalStorageService
    ) { }

    ngOnInit(): void {
        this.setCurrentTheme();
    }

    setCurrentTheme() {
        const currentTheme = this.storage.get<string>("theme") ?? "dark";
        this.setTheme(currentTheme);
    }

    setTheme(theme: string) {
        document.documentElement.setAttribute("data-theme", theme);
        this.storage.set("theme", theme);
    }
}
