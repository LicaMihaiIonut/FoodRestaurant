import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-review-add',
  templateUrl: './review-add.component.html',
  styleUrls: ['./review-add.component.css'],
})
export class ReviewAddComponent implements OnInit {
  public isLoading: boolean = false;
  public saveClicked: boolean = false;
  public selectedScore: number = -1;

  constructor(
    private dialogRef: MatDialogRef<ReviewAddComponent>,
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  public form = this.formBuilder.group({
    description: new FormControl(''),
  });

  public close(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {}

  public submit(): void {
    this.isLoading = true;

    if (this.selectedScore === -1) {
      this.saveClicked = true;
      this.isLoading = false;
      return;
    }

    if (!this.form.valid) {
      this.isLoading = false;
      return;
    }

    const post = {
      orderId: this.data?.orderId,
      description: this.form.controls['description'].value ?? '',
      score: this.selectedScore + 1,
    };

    this.orderService.placeReview(post).subscribe(() => {
      this.isLoading = false;
      this.dialogRef.close({
        grade: post.score,
        description: post.description,
      });
    });
  }

  public selectScore(score: number): void {
    this.selectedScore = this.selectedScore !== score ? score : -1;
  }
}
