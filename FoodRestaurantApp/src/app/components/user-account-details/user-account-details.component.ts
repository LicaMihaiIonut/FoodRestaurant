import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserAccountDetailsGetModel } from 'src/app/models/users/user-account-details-get.model';
import { UserService } from 'src/app/services/user.service';
import { UserAccountDetailsPopupComponent } from './user-account-details-popup/user-account-details-popup.component';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Observable, of, timer } from 'rxjs';

@Component({
  selector: 'app-user-account-details',
  templateUrl: './user-account-details.component.html',
  styleUrls: ['./user-account-details.component.css'],
})
export class UserAccountDetailsComponent implements OnInit {
  public accountDetails: UserAccountDetailsGetModel | null = null;
  constructor(
    private userService: UserService,
    public dialog: MatDialog,
    private snackbarService: SnackbarService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  public onOpenPopup(isName: boolean): void {
    const dialogRef = this.dialog.open(UserAccountDetailsPopupComponent, {
      width: '500px',
      data: {
        name: this.accountDetails?.name,
        phone: this.accountDetails?.phone,
        email: this.accountDetails?.email,
        isName: isName,
      },
    });

    dialogRef.afterClosed().subscribe((data: any) => {
      this.loadData();

      if (data.showMessage) {
        this.snackbarService.openSuccess(
          'Account details were successfully updated.'
        );
      }
    });
  }

  private loadData(): void {
    var user = this.userService.getUser();

    this.userService
      .getAccountDetails(user?.email ?? '')
      .subscribe((response: UserAccountDetailsGetModel) => {
        this.accountDetails = response;
      });
  }
}
