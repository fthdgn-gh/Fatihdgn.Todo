import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { LocalStorageService } from "./local-storage.service";
import { AuthLoginResponseDto } from "src/api/models";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    constructor(
    private readonly storageService: LocalStorageService
    ) {}
    intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const login = this.storageService.get<AuthLoginResponseDto>("login");
        if(login == null) return next.handle(req);
        // Apply the headers
        req = req.clone({
            setHeaders: {
                "Authorization": `Bearer ${login!.accessToken}`
            }
        });

        // Also handle errors globally
        return next.handle(req).pipe(
            catchError(err => {
                // eslint-disable-next-line no-console
                console.log("Error", err);
                return throwError(() => err);
            })
        );
    }
}