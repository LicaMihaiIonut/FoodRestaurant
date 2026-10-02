import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-custom-button',
  templateUrl: './custom-button.component.html',
  styleUrls: ['./custom-button.component.css'],
})
export class CustomButtonComponent implements OnInit {
  @Input() public icon: string = '';
  @Input() public text: string = '';
  @Input() public canClick: () => boolean = () => true;

  constructor() {}

  ngOnInit(): void {}
}
