import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  Validators,
} from '@angular/forms';
import { UserSecurityChangePasswordModel } from 'src/app/models/user-security/user-security-change-password.model';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-security',
  templateUrl: './user-security.component.html',
  styleUrls: ['./user-security.component.css'],
})
export class UserSecurityComponent implements OnInit {
  public form = this.formBuilder.group({
    currentPassword: new FormControl('', [Validators.required]),
    newPassword: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required),
  });

  public isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private snackbarService: SnackbarService,
    private userService: UserService
  ) {}

  ngOnInit(): void {}

  public updatePassword(): void {
    this.isLoading = true;

    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const data: UserSecurityChangePasswordModel = {
      email: this.userService.getUser()?.email ?? '',
      currentPassword: this.form.controls['currentPassword'].value,
      newPassword: this.form.controls['newPassword'].value,
      confirmPassword: this.form.controls['confirmPassword'].value,
    };

    this.userService.changePassword(data).subscribe({
      next: (_: any) => this.handleSuccess(),
      error: (error: any) => this.handleError(error),
    });
  }

  private handleSuccess(): void {
    window.location.reload();
    this.snackbarService.openSuccess('Password has been updated successfully.');
  }

  private handleError(error: any): void {
    this.isLoading = false;
    const errorMessage = error.error as string;

    if (!errorMessage) {
      return;
    }

    if (errorMessage.includes('UserEmailAlreadyExistsException')) {
      this.snackbarService.openError('Password is invalid.');
    }

    if (errorMessage.includes('UserRegistrationPasswordDoNotMatchException')) {
      this.snackbarService.openError('Passwords do not match.');
    }
  }
}
