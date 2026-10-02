import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class SnackbarService {
  constructor(private snackBar: MatSnackBar) {}

  public openError(error: string, duration: number = 5000): void {
    this.snackBar.open(error, undefined, {
      duration: duration,
      verticalPosition: 'top',
      horizontalPosition: 'right',
      panelClass: 'snackbar-error',
    });
  }

  public openSuccess(success: string, duration: number = 3000): void {
    this.snackBar.open(success, undefined, {
      duration: duration,
      verticalPosition: 'top',
      horizontalPosition: 'right',
      panelClass: 'snackbar-success',
    });
  }
}
