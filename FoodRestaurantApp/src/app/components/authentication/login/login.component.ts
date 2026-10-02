import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginGetModel } from 'src/app/models/users/user-login-get.model';
import { UserLoginModel } from 'src/app/models/users/user-login.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  public form = this.formBuilder.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  public isLoading: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService
  ) {}

  ngOnInit(): void {}

  public goToRegister(): void {
    this.router.navigateByUrl('register');
  }

  public login() {
    this.isLoading = true;

    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const data: UserLoginModel = {
      email: this.form.controls['email'].value,
      password: this.form.controls['password'].value,
    };

    this.userService.login(data).subscribe({
      next: (response: UserLoginGetModel) => {
        this.isLoading = false;
        this.userService.loginSucceeded(response);
      },
      error: (error: any) => {
        this.isLoading = false;
        this.userService.loginFailed(error);
      },
    });
  }
}
