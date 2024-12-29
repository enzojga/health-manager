import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from "../../../shared/person-card/person-card.component";
import { HttpService } from '../../../services/http.service';
import { Appointment, Patient } from '../../interfaces/external-interfaces';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css'],
  imports: [PersonCardComponent]
})
export class PatientsComponent implements OnInit {

  appointments: Appointment[] = [];

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.getPatiens();
  }

  getPatiens() {
    this.httpService.genericGet<Appointment[]>("appointments").subscribe((res: Appointment[]) => {
      this.appointments = res;
    })
  }

  getStatus(patient: Patient) {
    if(patient.roomId) {
      return "Internado";
    }
    if(patient.doctorId) {
      return "Check-in";
    }
    if(patient.nurseId) {
      return "Triagem";
    }
    return "Aguardando";
  }
}
