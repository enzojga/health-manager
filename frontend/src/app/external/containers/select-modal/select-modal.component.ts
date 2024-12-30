import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-select-modal',
  templateUrl: './select-modal.component.html',
  styleUrls: ['./select-modal.component.css'],
  imports: [
      MatButtonModule,
      MatDividerModule,
      MatIconModule,
  ]
})
export class SelectModalComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
