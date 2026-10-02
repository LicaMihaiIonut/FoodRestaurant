import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReviewGetListModel } from 'src/app/models/reviews/review-get-list.model';

@Component({
  selector: 'app-review-details',
  templateUrl: './review-details.component.html',
  styleUrls: ['./review-details.component.css'],
})
export class ReviewDetailsComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<ReviewDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ReviewGetListModel
  ) {}

  ngOnInit(): void {}

  public close(): void {
    this.dialogRef.close();
  }

  public canFillStar(grade: number, starIndex: number): boolean {
    return grade >= starIndex + 1;
  }
}
