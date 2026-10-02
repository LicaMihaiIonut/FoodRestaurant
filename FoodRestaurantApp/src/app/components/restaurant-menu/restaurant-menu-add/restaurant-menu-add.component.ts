import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CategoryGetListModel } from 'src/app/models/categories/category-get-list.model';
import { PageResultModel } from 'src/app/models/page-result.model';
import { RestaurantMenuGetListModel } from 'src/app/models/restaurant-menu/restaurant-menu-get-list.model';
import { RestaurantMenuPostModel } from 'src/app/models/restaurant-menu/restaurant-menu-post.model';
import { CategoryService } from 'src/app/services/category.service';
import { RestaurantMenuService } from 'src/app/services/restaurant-menu.service';

@Component({
  selector: 'app-restaurant-menu-add',
  templateUrl: './restaurant-menu-add.component.html',
  styleUrls: ['./restaurant-menu-add.component.css'],
})
export class RestaurantMenuAddComponent implements OnInit {
  public rawImage: any = null;
  public categories: CategoryGetListModel[] = [];

  public isLoading: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<RestaurantMenuAddComponent>,
    private formBuilder: FormBuilder,
    private restaurantMenuService: RestaurantMenuService,
    private categoryService: CategoryService,
    @Inject(MAT_DIALOG_DATA) public data: RestaurantMenuGetListModel
  ) {}

  public form = this.formBuilder.group({
    categoryId: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    price: new FormControl('', Validators.required),
    discount: new FormControl(''),
  });

  public ngOnInit(): void {
    if (this.data) {
      this.form.controls['categoryId'].setValue(this.data.categoryId);
      this.form.controls['name'].setValue(this.data.name ?? '');
      this.form.controls['price'].setValue(this.data.price ?? '');
      this.form.controls['discount'].setValue(this.data.discount ?? '');
    }

    this.categoryService
      .getAll('', null)
      .subscribe((response: PageResultModel<CategoryGetListModel>) => {
        this.categories = response.entities;
      });
  }

  public close(): void {
    this.dialogRef.close();
  }

  public submit(): void {
    this.isLoading = true;

    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const data: RestaurantMenuPostModel = {
      restaurantMenuId: this.data?.restaurantMenuId,
      name: this.form.controls['name'].value,
      price: this.form.controls['price'].value,
      discount: !!this.form.controls['discount'].value
        ? this.form.controls['discount'].value
        : null,
      categoryId: this.form.controls['categoryId'].value,
    };

    console.log(data);

    this.restaurantMenuService.post(data).subscribe(() => {
      this.isLoading = false;
      this.close();
    });
  }
}
