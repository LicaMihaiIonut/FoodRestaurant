import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-view-management',
  templateUrl: './view-management.component.html',
  styleUrls: ['./view-management.component.css'],
})
export class ViewManagementComponent implements OnInit {
  @Input() public set nrOfRecords(value: number) {
    this.calculateNrOfPages(value);
  }

  public currentPage = 1;
  public searchText: string = '';
  public nrOfPages: number = 1;

  @Output() public viewManagementChanged: EventEmitter<any> =
    new EventEmitter();

  constructor() {}

  public ngOnInit(): void {
    this.calculateNrOfPages(this.nrOfRecords);
  }

  public search(): void {
    this.viewManagementChanged.emit({
      searchText: this.searchText,
      currentPage: this.currentPage,
    });
  }

  public paginationNext(): void {
    if (this.canPaginationNext()) {
      this.currentPage++;
      this.publishViewManagementChanged();
    }
  }

  public canPaginationNext(): boolean {
    return this.currentPage < this.nrOfPages;
  }

  public paginationPrevious(): void {
    if (this.canPaginationPrevious()) {
      this.currentPage--;
      this.publishViewManagementChanged();
    }
  }

  public canPaginationPrevious(): boolean {
    return this.currentPage > 1;
  }

  public paginationFirst(): void {
    if (this.canPaginationFirst()) {
      this.currentPage = 1;
      this.publishViewManagementChanged();
    }
  }

  public canPaginationFirst(): boolean {
    return this.currentPage !== 1;
  }

  public paginationLast(): void {
    if (this.canPaginationLast()) {
      this.currentPage = this.nrOfPages;
      this.publishViewManagementChanged();
    }
  }

  public canPaginationLast(): boolean {
    return this.currentPage !== this.nrOfPages;
  }

  private publishViewManagementChanged(): void {
    this.viewManagementChanged.emit({
      searchText: this.searchText,
      currentPage: this.currentPage,
    });
  }

  private calculateNrOfPages(nrOfRecords: number): void {
    if (!nrOfRecords) {
      this.nrOfPages = 1;
      return;
    }

    const nrOfPages =
      nrOfRecords % 10 === 0 ? nrOfRecords / 10 : nrOfRecords / 10 + 1;
    this.nrOfPages = Math.trunc(nrOfPages);
  }
}
