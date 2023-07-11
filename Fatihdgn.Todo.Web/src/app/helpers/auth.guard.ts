import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { LocalStorageService } from "./local-storage.service";
import { AuthLoginResponseDto } from "src/api/models";

@Injectable({
    providedIn: "root"
})
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private storageService: LocalStorageService) {}

    canActivate(): boolean {
        const login = this.storageService.get<AuthLoginResponseDto>("login");

        if (login?.accessToken) {
            return true;
        } else {
            this.router.navigate(["/account/login"]);
            return false;
        }
    }
}