import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css'],
})
export class UserAccountComponent implements OnInit {
  public menuItemActive: string = 'Security';
  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    const menuItemCached = localStorage.getItem('userAccountMenu');
    if (menuItemCached) {
      this.menuItemActive = menuItemCached;
    }
  }

  public setMenuItemActive(menuItem: string): void {
    this.menuItemActive = menuItem;
    localStorage.setItem('userAccountMenu', this.menuItemActive);
  }

  public isMenuItemActive(menuItem: string): boolean {
    return this.menuItemActive === menuItem;
  }

  public logout(): void {
    this.userService.logout();
  }

  public goBack(): void {
    this.router.navigate(['restaurants']);
  }
}
