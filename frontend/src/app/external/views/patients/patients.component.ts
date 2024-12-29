import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from "../../../shared/person-card/person-card.component";
import { HttpService } from '../../../services/http.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css'],
  imports: [PersonCardComponent]
})
export class PatientsComponent implements OnInit {

  constructor(private httpService: HttpService) { }

  ngOnInit() {
    this.getPatiens();
  }

  getPatiens() {
    this.httpService.genericGet("Patient").subscribe(res => {
      console.log(res)
    })
  }

}
