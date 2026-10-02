import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginGetModel } from 'src/app/models/users/user-login-get.model';
import { UserRegisterModel } from 'src/app/models/users/user-register.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public form = this.formBuilder.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required),
    isRestaurant: new FormControl(),
  });

  public isLoading: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService
  ) {}

  ngOnInit(): void {}

  public goToLogin(): void {
    this.router.navigateByUrl('login');
  }

  public register(): void {
    this.isLoading = true;

    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const data: UserRegisterModel = {
      email: this.form.controls['email'].value,
      isRestaurant: this.form.controls['isRestaurant'].value ?? false,
      confirmPassword: this.form.controls['confirmPassword'].value,
      password: this.form.controls['password'].value,
    };

    this.userService.register(data).subscribe({
      next: (response: UserLoginGetModel) => {
        this.isLoading = false;
        this.userService.registerSucceeded(response);
      },
      error: (error: any) => {
        this.isLoading = false;
        this.userService.registerFailed(error);
      },
    });
  }
}
