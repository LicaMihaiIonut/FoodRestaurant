import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageResultModel } from 'src/app/models/page-result.model';
import { ReviewGetListModel } from 'src/app/models/reviews/review-get-list.model';
import { ReviewService } from 'src/app/services/review.service';
import { ReviewDetailsComponent } from '../review-details/review-details.component';
import { ReviewStatisticsModel } from 'src/app/models/reviews/review-statistics.model';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css'],
})
export class ReviewsComponent implements OnInit {
  private _searchText: string = '';
  private _currentPage: number = 1;

  public statistics: ReviewStatisticsModel = new ReviewStatisticsModel();
  public reviews: ReviewGetListModel[] = [];
  public nrOfRecords: number = 0;

  constructor(private reviewService: ReviewService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.load();
    this.getStatistics();
  }

  public viewManagementChanged($event: any): void {
    this._searchText = $event.searchText;
    this._currentPage = $event.currentPage;

    this.load();
  }

  public openReviewPopup(review: ReviewGetListModel): void {
    this.dialog.open(ReviewDetailsComponent, {
      width: '500px',
      data: review,
    });
  }

  public getDate(date: Date): string | null {
    const datePipe = new DatePipe('en-US');
    return datePipe.transform(date, 'short');
  }

  public canFillStar(grade: number, starIndex: number): boolean {
    return grade >= starIndex + 1;
  }

  public getDescriptionPreview(description: string): string {
    return description.length > 50
      ? `${description.substring(0, 48)}...`
      : description;
  }

  private load(): void {
    this.reviewService
      .getAll(this._searchText, this._currentPage)
      .subscribe((response: PageResultModel<ReviewGetListModel>) => {
        this.reviews = response.entities;
        this.nrOfRecords = response.numberOfRecords;
      });
  }

  private getStatistics(): void {
    this.reviewService
      .getStatistics()
      .subscribe((response: ReviewStatisticsModel) => {
        this.statistics = response;
      });
  }
}
