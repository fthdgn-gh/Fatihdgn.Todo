import { Component } from '@angular/core';
import { AuthService } from 'src/api/services';
import { LocalStorageService } from '../helpers/local-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'todo-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(
    private readonly storage: LocalStorageService,
    private readonly router: Router
  ) {
    
  }
  isDrawerOpened = false;

  logout(){
    this.storage.remove("login");
    this.router.navigate(["/account/login"]);
  }
}
