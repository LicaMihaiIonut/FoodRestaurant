import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoryGetListModel } from 'src/app/models/categories/category-get-list.model';
import { CategoryAddEditModel } from 'src/app/models/categories/category-add-edit.model';
import { CategoryService } from 'src/app/services/category.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-add-edit',
  templateUrl: './category-add-edit.component.html',
  styleUrls: ['./category-add-edit.component.css'],
})
export class CategoryAddEditComponent implements OnInit {
  private _categoryId: number = 0;

  public isLoading: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<CategoryAddEditComponent>,
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    @Inject(MAT_DIALOG_DATA) public data: CategoryGetListModel
  ) {
    this._categoryId = data?.categoryId;
  }

  public form = this.formBuilder.group({
    name: new FormControl(this.data?.name ?? '', [Validators.required]),
    isAvailable: new FormControl(this.data?.isAvailable ?? false),
  });

  public ngOnInit(): void {}

  public close(): void {
    this.dialogRef.close();
  }

  public submit(): void {
    this.isLoading = true;

    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const data = this.getData();

    this.send(data).subscribe(() => {
      this.isLoading = false;
      this.close();
    });
  }

  public getTitle(): string {
    const title = this._categoryId ? 'Edit' : 'Add';

    return `${title} category`;
  }

  private getData(): CategoryAddEditModel {
    const data: CategoryAddEditModel = {
      categoryId: this._categoryId,
      name: this.form.controls['name'].value,
      isAvailable: this.form.controls['isAvailable'].value ?? false,
    };

    return data;
  }

  private send(data: CategoryAddEditModel): Observable<any> {
    return this._categoryId
      ? this.categoryService.patch(data)
      : this.categoryService.post(data);
  }
}
