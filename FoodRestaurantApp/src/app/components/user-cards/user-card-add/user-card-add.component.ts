import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-card-add',
  templateUrl: './user-card-add.component.html',
  styleUrls: ['./user-card-add.component.css'],
})
export class UserCardAddComponent {
  public form = this.formBuilder.group({
    cardNumber: new FormControl(this.data?.cardNumber ?? '', [
      Validators.required,
    ]),
    monthExpiration: new FormControl(this.data?.monthExpiration ?? '', [
      Validators.required,
    ]),
    yearExpiration: new FormControl(this.data?.yearExpiration ?? '', [
      Validators.required,
    ]),
    securityCode: new FormControl(this.data?.securityCode ?? '', [
      Validators.required,
    ]),
    owner: new FormControl(this.data?.owner ?? '', [Validators.required]),
  });

  public isLoading: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<UserCardAddComponent>,
    private formBuilder: FormBuilder,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: any
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

    const data = {
      userCardId: this.data?.userCardId,
      cardNumber: `${this.form.controls['cardNumber'].value}`,
      monthExpiration: this.form.controls['monthExpiration'].value,
      yearExpiration:
        this.data?.yearExpiration ?? this.form.controls['yearExpiration'].value,
      securityCode: this.form.controls['securityCode'].value,
      owner: this.form.controls['owner'].value,
    };

    this.userService.addOrUpdateCard(data).subscribe((_: any) => {
      this.isLoading = false;
      this.dialogRef.close({ showMessage: true });
    });
  }

  public getMonths(): number[] {
    var months = [];

    for (let i = 1; i <= 12; i++) {
      months.push(i);
    }

    return months;
  }

  public getYears(): number[] {
    var years = [];
    const date = new Date();

    for (let i = 0; i <= 10; i++) {
      years.push(date.getFullYear() + i);
    }

    return years;
  }

  public getTitle(): string {
    return this.data?.userCardId ? 'Update card' : 'Add card';
  }
}
