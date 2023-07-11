import { Component, Input, TemplateRef, ViewEncapsulation } from "@angular/core";
import { Observable } from "rxjs";
import { NavigationService } from "src/app/helpers/navigatation.service";
import { SideNavDirection } from "./sidenav-direction";

// https://itnext.io/simple-sliding-side-bar-for-your-angular-web-apps-d54fef7c1654
@Component({
    selector: "todo-side-nav",
    templateUrl: "./sidenav.component.html",
    styleUrls: ["./sidenav.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class SideNavComponent {

    showSideNav: Observable<boolean>;

  @Input() sidenavTemplateRef: TemplateRef<unknown> | null = null;
  @Input() duration: number = 0.25;
  @Input() navWidth: number = window.innerWidth;
  @Input() direction: SideNavDirection = "left";
  
  constructor(private navService: NavigationService) {
      this.showSideNav = this.navService.getShowNav();
  }

  onSidebarClose() {
      this.navService.setShowNav(false);
  }

  getSideNavBarStyle(showNav: boolean | null) {
      const navBarStyle: { transition?: string, width?: string, left?: string, right?: string } = {};
    
      navBarStyle.transition = this.direction + " " + this.duration + "s, visibility " + this.duration + "s";
      navBarStyle.width = this.navWidth + "px";
      navBarStyle[this.direction] = (showNav ? 0 : (this.navWidth * -1)) + "px";
    
      return navBarStyle;
  }
}