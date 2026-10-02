import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserAccountDetailsGetModel } from 'src/app/models/users/user-account-details-get.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-account-details-popup',
  templateUrl: './user-account-details-popup.component.html',
  styleUrls: ['./user-account-details-popup.component.css'],
})
export class UserAccountDetailsPopupComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<UserAccountDetailsPopupComponent>,
    private formBuilder: FormBuilder,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  public form = this.data.isName
    ? this.formBuilder.group({
        name: new FormControl(this.data?.name ?? '', [Validators.required]),
      })
    : this.formBuilder.group({
        phone: new FormControl(this.data?.phone ?? '', [Validators.required]),
      });

  ngOnInit(): void {}

  public close(showMessage: boolean = false): void {
    this.dialogRef.close({ showMessage: showMessage });
  }

  public submit(): void {
    if (!this.form.valid) {
      return;
    }

    const data = this.getData();

    this.userService.changeAccountDetails(data).subscribe(() => {
      this.close(true);
    });
  }

  public getTitle(): string {
    return `Change your ${this.data.isName ? 'name' : 'phone number'}`;
  }

  private getData(): UserAccountDetailsGetModel {
    const data: UserAccountDetailsGetModel = {
      name: this.data.isName
        ? this.form.controls['name'].value
        : this.data?.name,
      phone: !this.data.isName
        ? `${this.form.controls['phone'].value}`
        : this.data?.phone,
      email: this.data.email,
    };

    return data;
  }
}
