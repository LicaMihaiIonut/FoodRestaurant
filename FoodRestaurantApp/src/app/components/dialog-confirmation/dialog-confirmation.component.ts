import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConfirmationDialogModel } from 'src/app/models/confirmation-dialog/confirmation-dialog.model';

@Component({
  selector: 'app-dialog-confirmation',
  templateUrl: './dialog-confirmation.component.html',
  styleUrls: ['./dialog-confirmation.component.css'],
})
export class DialogConfirmationComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<DialogConfirmationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  public ngOnInit(): void {}

  public close(confirmed: boolean): void {
    const response: ConfirmationDialogModel = {
      confirmed: confirmed,
    };

    this.dialogRef.close(response);
  }

  public getMessage(): string {
    return (
      this.data?.message ??
      'Are you sure you want to delete the selected lines?'
    );
  }
}
