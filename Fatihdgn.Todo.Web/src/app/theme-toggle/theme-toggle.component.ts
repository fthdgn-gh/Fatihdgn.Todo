import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'fd-theme-toggle',
  templateUrl: './theme-toggle.component.html',
  styleUrls: ['./theme-toggle.component.css'],
})
export class ThemeToggleComponent {
  public get currentTheme(): Theme {
    return document.documentElement.classList.contains('dark')
      ? 'dark'
      : 'light';
  }
  public set currentTheme(v: Theme) {
    if (v === 'dark') document.documentElement.classList.add('dark');
    else document.documentElement.classList.remove('dark');
  }

  onToggleTheme() {
    switch (this.currentTheme) {
      case 'light':
        this.currentTheme = 'dark';
        break;
      case 'dark':
        this.currentTheme = 'light';
        break;
    }
  }
}

export type Theme = 'light' | 'dark';
