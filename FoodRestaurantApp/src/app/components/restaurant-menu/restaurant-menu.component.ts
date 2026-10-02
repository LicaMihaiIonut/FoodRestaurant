import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RestaurantMenuGetListModel } from 'src/app/models/restaurant-menu/restaurant-menu-get-list.model';
import { RestaurantMenuService } from 'src/app/services/restaurant-menu.service';
import { RestaurantMenuAddComponent } from './restaurant-menu-add/restaurant-menu-add.component';
import { DialogConfirmationComponent } from '../dialog-confirmation/dialog-confirmation.component';
import { ConfirmationDialogModel } from 'src/app/models/confirmation-dialog/confirmation-dialog.model';
import { PageResultModel } from 'src/app/models/page-result.model';

@Component({
  selector: 'app-restaurant-menu',
  templateUrl: './restaurant-menu.component.html',
  styleUrls: ['./restaurant-menu.component.css'],
})
export class RestaurantMenuComponent implements OnInit {
  public activeTab: string = 'Categories';

  private localStorageTabActiveKey: string = 'restaurantMenuTabActive';

  constructor() {}

  public ngOnInit(): void {
    this.activeTab =
      localStorage.getItem(this.localStorageTabActiveKey) ?? 'Categories';
  }

  public isTabActive(tab: string): boolean {
    return this.activeTab === tab;
  }

  public setTabActive(tab: string): void {
    this.activeTab = tab;
    localStorage.setItem(this.localStorageTabActiveKey, this.activeTab);
  }
}
