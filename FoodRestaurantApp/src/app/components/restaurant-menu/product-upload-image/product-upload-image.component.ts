import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RestaurantMenuService } from 'src/app/services/restaurant-menu.service';

@Component({
  selector: 'app-product-upload-image',
  templateUrl: './product-upload-image.component.html',
  styleUrls: ['./product-upload-image.component.css'],
})
export class ProductUploadImageComponent implements OnInit {
  public rawImage: any = null;

  public form = this.formBuilder.group({
    image: null,
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ProductUploadImageComponent>,
    private restaurantMenuService: RestaurantMenuService
  ) {}

  ngOnInit(): void {
    if (this.data.image) {
      this.form.controls['image'].setValue(this.data.image);
    }
  }

  public removeImage(): void {
    this.form.controls['image'].setValue(null);
    this.rawImage = null;
  }

  public hasImage(): boolean {
    return this.form.controls['image'].value;
  }

  public selectFile($event: any) {
    if (!$event) {
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL($event.target.files[0]);
    reader.onload = (_) => {
      this.form.controls['image'].setValue(reader.result);
    };
  }

  public close(): void {
    this.dialogRef.close();
  }

  public submit(): void {
    if (!this.form.valid) {
      return;
    }

    const data = {
      menuRestaurantId: this.data.restaurantMenuId,
      image: this.form.controls['image'].value ?? '',
    };

    this.restaurantMenuService.uploadPicture(data).subscribe((_: any) => {
      this.close();
    });
  }
}
