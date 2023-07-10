import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private storageService: LocalStorageService) {}

  canActivate(): boolean {
    const accessToken = this.storageService.get<string>('accessToken');

    if (accessToken) {
      return true;
    } else {
      this.router.navigate(['/account/login']);
      return false;
    }
  }
}