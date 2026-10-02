import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserAddressGetListModel } from 'src/app/models/users/user-address-get-list.model';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserService } from 'src/app/services/user.service';
import { UserAddressAddComponent } from './user-address-add/user-address-add.component';
import { DialogConfirmationComponent } from '../dialog-confirmation/dialog-confirmation.component';
import { ConfirmationDialogModel } from 'src/app/models/confirmation-dialog/confirmation-dialog.model';

@Component({
  selector: 'app-user-addresses',
  templateUrl: './user-addresses.component.html',
  styleUrls: ['./user-addresses.component.css'],
})
export class UserAddressesComponent implements OnInit {
  public addresses: UserAddressGetListModel[] = [];

  constructor(
    private userService: UserService,
    public dialog: MatDialog,
    private snackbarService: SnackbarService
  ) {}

  ngOnInit(): void {
    this.load();
  }

  public add(): void {
    const dialogRef = this.dialog.open(UserAddressAddComponent, {
      width: '500px',
    });

    dialogRef.afterClosed().subscribe((data: any) => {
      this.load();

      if (data?.showMessage) {
        this.snackbarService.openSuccess('Address is successfully added.');
      }
    });
  }

  public edit(address: UserAddressGetListModel): void {
    const dialogRef = this.dialog.open(UserAddressAddComponent, {
      width: '500px',
      data: address,
    });

    dialogRef.afterClosed().subscribe((data: any) => {
      this.load();

      if (data?.showMessage) {
        this.snackbarService.openSuccess('Address is successfully updated.');
      }
    });
  }

  public delete(addressId: number): void {
    const dialogRef = this.dialog.open(DialogConfirmationComponent, {
      width: '500px',
      data: { message: 'Are you sure you want to delete the address?' },
    });

    dialogRef.afterClosed().subscribe((response: ConfirmationDialogModel) => {
      if (response.confirmed) {
        this.userService.deleteAddress(addressId).subscribe(() => {
          this.load();
        });
      }
    });
  }

  private load(): void {
    this.userService
      .getAddresses()
      .subscribe((response: UserAddressGetListModel[]) => {
        this.addresses = response;
      });
  }
}
