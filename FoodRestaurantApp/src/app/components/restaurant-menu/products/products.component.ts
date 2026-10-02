import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageResultModel } from 'src/app/models/page-result.model';
import { RestaurantMenuGetListModel } from 'src/app/models/restaurant-menu/restaurant-menu-get-list.model';
import { RestaurantMenuService } from 'src/app/services/restaurant-menu.service';
import { RestaurantMenuAddComponent } from '../restaurant-menu-add/restaurant-menu-add.component';
import { DialogConfirmationComponent } from '../../dialog-confirmation/dialog-confirmation.component';
import { ConfirmationDialogModel } from 'src/app/models/confirmation-dialog/confirmation-dialog.model';
import { DatePipe } from '@angular/common';
import { ProductUploadImageComponent } from '../product-upload-image/product-upload-image.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  private _searchText: string = '';
  private _currentPage: number = 1;

  public restaurantMenus: RestaurantMenuGetListModel[] = [];
  public selectAllCheckbox: boolean = false;
  public nrOfRecords: number = 0;

  constructor(
    private restaurantMenuService: RestaurantMenuService,
    public dialog: MatDialog
  ) {}

  public ngOnInit(): void {
    this.load();
  }

  public viewManagementChanged($event: any): void {
    this._searchText = $event.searchText;
    this._currentPage = $event.currentPage;

    this.load();
  }

  public edit(restaurantMenu: RestaurantMenuGetListModel): void {
    const ref = this.dialog.open(RestaurantMenuAddComponent, {
      data: restaurantMenu,
    });

    ref.afterClosed().subscribe((_: any) => {
      this.load();
    });
  }

  public load(): void {
    this.restaurantMenuService
      .getAll(this._searchText, this._currentPage)
      .subscribe((response: PageResultModel<RestaurantMenuGetListModel>) => {
        this.restaurantMenus = response.entities;
        this.nrOfRecords = response.numberOfRecords;

        this.selectAllCheckbox = false;
      });
  }

  public add(): void {
    const dialogRef = this.dialog.open(RestaurantMenuAddComponent, {
      width: '500px',
    });

    dialogRef.afterClosed().subscribe((_) => {
      this.ngOnInit();
    });
  }

  public remove(): void {
    if (!this.canRemove()) {
      return;
    }

    const selectedIds = this.restaurantMenus
      .filter((x) => x.isSelected)
      .map(
        (restaurantMenu: RestaurantMenuGetListModel) =>
          restaurantMenu.restaurantMenuId
      );

    const dialogRef = this.dialog.open(DialogConfirmationComponent);

    dialogRef.afterClosed().subscribe((response: ConfirmationDialogModel) => {
      if (response.confirmed) {
        this.restaurantMenuService.remove(selectedIds).subscribe(() => {
          this.ngOnInit();
          this.selectAllCheckbox = false;
        });
      }
    });
  }

  public selectAll(): void {
    const selected =
      this.restaurantMenus.filter((x) => !x.isSelected).length > 0;

    this.restaurantMenus.map((restaurantMenu: RestaurantMenuGetListModel) => {
      restaurantMenu.isSelected = selected;
    });
  }

  public canRemove(): boolean {
    return this.restaurantMenus.filter((x) => x.isSelected).length > 0;
  }

  public getDiscount(discount: number | undefined): string | null {
    return !!discount ? `${discount}%` : '';
  }

  public uploadImage(restaurantMenu: RestaurantMenuGetListModel): void {
    const ref = this.dialog.open(ProductUploadImageComponent, {
      data: {
        restaurantMenuId: restaurantMenu.restaurantMenuId,
        image: restaurantMenu.image,
      },
    });

    ref.afterClosed().subscribe((_: any) => {
      this.load();
    });
  }
}
