import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { RestaurantProfileGetModel } from 'src/app/models/restaurants/restaurant-profile-get.model';
import { RestaurantProfilePostModel } from 'src/app/models/restaurants/restaurant-profile-post.model';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  public rawImage: any = null;
  public profile: RestaurantProfileGetModel;

  public form = this.formBuilder.group({
    name: new FormControl('', [Validators.required]),
    delivery: new FormControl('', Validators.required),
    transport: new FormControl('', Validators.required),
    image: new FormControl(null),
  });

  constructor(
    private formBuilder: FormBuilder,
    private restaurantService: RestaurantService,
    private snackBarService: SnackbarService
  ) {}

  ngOnInit(): void {
    this.restaurantService
      .getProfile()
      .subscribe((response: RestaurantProfileGetModel) => {
        this.profile = response;

        this.form.controls['image'].setValue(response.image ?? '');
        this.form.controls['name'].setValue(response.name ?? '');
        this.form.controls['delivery'].setValue(response.delivery ?? '');
        this.form.controls['transport'].setValue(response.transport ?? '');
      });
  }

  public submit(): void {
    if (!this.form.valid) {
      return;
    }

    const data: RestaurantProfilePostModel = {
      image: this.form.controls['image'].value,
      name: this.form.controls['name'].value,
      delivery: this.form.controls['delivery'].value,
      transport: this.form.controls['transport'].value,
    };

    this.restaurantService.updateProfile(data).subscribe((_: any) => {
      this.profile.isValidated = true;

      localStorage.setItem('restaurant-image', data.image ?? '');
      localStorage.setItem('restaurant-name', data.name ?? '');

      this.snackBarService.openSuccess(
        'Restaurant information has been saved.'
      );
    });
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
}
