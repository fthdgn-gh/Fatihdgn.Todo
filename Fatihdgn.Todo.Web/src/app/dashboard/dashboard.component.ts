import { Component, OnInit } from '@angular/core';
import { AuthService, ListsService } from 'src/api/services';
import { LocalStorageService } from '../helpers/local-storage.service';
import { Router } from '@angular/router';
import { NavigationService } from '../helpers/navigatation.service';
import { StateManager } from '../helpers/state.manager';
import { StateService } from '../helpers/state.service';

@Component({
  selector: 'todo-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(
    private readonly storage: LocalStorageService,
    private readonly router: Router,
    private readonly navService: NavigationService,
    public readonly state: StateService,
    private readonly stateManager: StateManager
  ) {
    
  }

  async ngOnInit(): Promise<void> {
    await this.stateManager.initAsync();
  }

  logout(){
    this.storage.remove("login");
    this.router.navigate(["/account/login"]);
  }

  toggleSideNav() {
    this.navService.setShowNav(true);
  }
}
