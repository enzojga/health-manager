import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from "../../../shared/person-card/person-card.component";

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css'],
  imports: [PersonCardComponent]
})
export class PatientsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    console.log('iniciei')
  }

}
