import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserAddressGetListModel } from 'src/app/models/users/user-address-get-list.model';
import { UserAddressPostPatchModel } from 'src/app/models/users/user-address-post-patch.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-address-add',
  templateUrl: './user-address-add.component.html',
  styleUrls: ['./user-address-add.component.css'],
})
export class UserAddressAddComponent {
  public form = this.formBuilder.group({
    address: new FormControl(this.data?.address ?? '', [Validators.required]),
    street: new FormControl(this.data?.street ?? '', [Validators.required]),
    city: new FormControl(this.data?.city ?? '', [Validators.required]),
  });

  public isLoading: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<UserAddressAddComponent>,
    private formBuilder: FormBuilder,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: UserAddressGetListModel
  ) {}

  public close(showMessage: boolean = false): void {
    this.dialogRef.close({ showMessage: showMessage });
  }

  public submit(): void {
    this.isLoading = true;
    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const data: UserAddressPostPatchModel = {
      userAddressId: this.data?.userAddressId,
      street: this.form.controls['street'].value,
      city: this.form.controls['city'].value,
      address: this.form.controls['address'].value,
    };

    this.userService.addOrUpdateAddress(data).subscribe((_: any) => {
      this.isLoading = false;
      this.dialogRef.close({ showMessage: true });
    });
  }

  public getTitle(): string {
    return this.data?.userAddressId ? 'Update address' : 'Add address';
  }
}
