import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CategoryGetListModel } from 'src/app/models/categories/category-get-list.model';
import { PageResultModel } from 'src/app/models/page-result.model';
import { CategoryService } from 'src/app/services/category.service';
import { CategoryAddEditComponent } from '../category-add-edit/category-add-edit.component';
import { DialogConfirmationComponent } from '../../dialog-confirmation/dialog-confirmation.component';
import { ConfirmationDialogModel } from 'src/app/models/confirmation-dialog/confirmation-dialog.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  private _searchText: string = '';
  private _currentPage: number = 1;

  public nrOfRecords: number = 0;
  public selectAllCheckbox: boolean = false;
  public categories: CategoryGetListModel[] = [];

  constructor(
    private categoryService: CategoryService,
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

  load() {
    this.categoryService
      .getAll(this._searchText, this._currentPage)
      .subscribe((response: PageResultModel<CategoryGetListModel>) => {
        this.categories = response.entities;
        this.nrOfRecords = response.numberOfRecords;

        this.selectAllCheckbox = false;
      });
  }

  public add(): void {
    const dialogRef = this.dialog.open(CategoryAddEditComponent, {
      width: '500px',
    });

    dialogRef.afterClosed().subscribe((_) => {
      this.load();
    });
  }

  public edit(categoryId: number): void {
    const category = this.categories.filter(
      (x) => x.categoryId === categoryId
    )[0];

    const dialogRef = this.dialog.open(CategoryAddEditComponent, {
      width: '500px',
      data: category,
    });

    dialogRef.afterClosed().subscribe((_) => {
      this.load();
    });
  }

  public remove(): void {
    const selectedIds = this.categories
      .filter((x) => x.isSelected)
      .map((category: CategoryGetListModel) => category.categoryId);

    const dialogRef = this.dialog.open(DialogConfirmationComponent);

    dialogRef.afterClosed().subscribe((response: ConfirmationDialogModel) => {
      if (response.confirmed) {
        this.categoryService.remove(selectedIds).subscribe(() => {
          this.load();
          this.selectAllCheckbox = false;
        });
      }
    });
  }

  public canRemove(): boolean {
    return this.categories.filter((x) => x.isSelected).length > 0;
  }

  public selectAll(): void {
    const selected = this.categories.filter((x) => !x.isSelected).length > 0;

    this.categories.map((restaurantMenu: CategoryGetListModel) => {
      restaurantMenu.isSelected = selected;
    });
  }
}
