import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserCardAddComponent } from './user-card-add/user-card-add.component';
import { UserService } from 'src/app/services/user.service';
import { UserCardGetList } from 'src/app/models/users/user-card-get-list.model';
import { DialogConfirmationComponent } from '../dialog-confirmation/dialog-confirmation.component';
import { ConfirmationDialogModel } from 'src/app/models/confirmation-dialog/confirmation-dialog.model';

@Component({
  selector: 'app-user-cards',
  templateUrl: './user-cards.component.html',
  styleUrls: ['./user-cards.component.css'],
})
export class UserCardsComponent implements OnInit {
  public cards: UserCardGetList[] = [];

  constructor(
    public dialog: MatDialog,
    private snackbarService: SnackbarService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  public onOpenPopup(): void {
    const dialogRef = this.dialog.open(UserCardAddComponent, {
      width: '500px',
    });

    dialogRef.afterClosed().subscribe((data: any) => {
      this.loadData();

      if (data?.showMessage) {
        this.snackbarService.openSuccess('Card is successfully added.');
      }
    });
  }

  public edit(card: UserCardGetList): void {
    const dialogRef = this.dialog.open(UserCardAddComponent, {
      width: '500px',
      data: card,
    });

    dialogRef.afterClosed().subscribe((data: any) => {
      this.loadData();

      if (data?.showMessage) {
        this.snackbarService.openSuccess('Card is successfully updated.');
      }
    });
  }

  public delete(cardId: number): void {
    const dialogRef = this.dialog.open(DialogConfirmationComponent, {
      width: '500px',
      data: { message: 'Are you sure you want to delete the card?' },
    });

    dialogRef.afterClosed().subscribe((response: ConfirmationDialogModel) => {
      if (response.confirmed) {
        this.userService.deleteCard(cardId).subscribe(() => {
          this.loadData();
        });
      }
    });
  }

  public getMonth(month: number): string {
    return month < 10 ? `0${month}` : `${month}`;
  }

  private loadData(): void {
    this.userService.getUserCards().subscribe((response: UserCardGetList[]) => {
      this.cards = response;
      console.log(this.cards);
    });
  }
}
