import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { LocalStorageService } from "./local-storage.service";
import { AuthLoginResponseDto } from "src/api/models";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(
    private readonly storageService: LocalStorageService
  ) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let login = this.storageService.get<AuthLoginResponseDto>('login');
    if(login == null) next.handle(req);
    // Apply the headers
    req = req.clone({
      setHeaders: {
        "Authorization": `Bearer ${login!.accessToken}`
      }
    });

    // Also handle errors globally
    return next.handle(req).pipe(
      tap(x => x, err => {
        // Handle this err
        console.error(`Error performing request, status code = ${err.status}`);
      })
    );
  }
}