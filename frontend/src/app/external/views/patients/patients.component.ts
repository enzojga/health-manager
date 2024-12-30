import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from "../../../shared/person-card/person-card.component";
import { HttpService } from '../../../services/http.service';
import { Appointment, Patient, PatientDTO } from '../../interfaces/external-interfaces';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { CreateModalComponent } from '../../containers/create-modal/create-modal.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css'],
  imports: [
    PersonCardComponent,
    MatIconModule,
    MatMenuModule,
    MatTooltipModule,
    MatButtonModule
  ],
})
export class PatientsComponent implements OnInit {

  appointments: Appointment[] = [];

  constructor(
    private httpService: HttpService,
    private readonly dialog: MatDialog,
    private toastr: ToastrService
  ) {}

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

  createPaitient() {
    const dialog = this.dialog.open(CreateModalComponent, {
      width: '476',
      height: '237px',
      data: {
        isPatient: true,
        title: "Cadastrar paciente"
      }
    });

    dialog.afterClosed().subscribe((res: PatientDTO) => {
      if(res) {
        this.createPatient(res);
      }
    })
  }

  createPatient(body: PatientDTO) {
    this.httpService.genericPost<Patient>("patient", body).subscribe((res: Patient) => {
      this.creatApointment(res.id);
    },
    (err) => {
      this.toastr.error('Algo deu errado ao cadastrar paciente.', 'Tente novamente');
    });
  }

  creatApointment(userId: number) {
    const body = {
      userId
    };
    this.httpService.genericPost("appointments", body).subscribe((res) => {
      this.toastr.success('Consulta iniciada com sucesso.', 'Sucesso');
      this.getPatiens();
    },
    (err) => {
      this.toastr.error('Algo deu errado ao iniciar consulta.', 'Tente novamente');
    });
  }
}
