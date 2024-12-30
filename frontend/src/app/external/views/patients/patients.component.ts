import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from "../../../shared/person-card/person-card.component";
import { HttpService } from '../../../services/http.service';
import { Appointment, Patient, PatientDTO } from '../../interfaces/external-interfaces';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { CreateModalComponent } from '../../../shared/create-modal/create-modal.component';
import { ToastrService } from 'ngx-toastr';
import { SelectModalComponent } from '../../../shared/select-modal/select-modal.component';

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
  patients: Patient[] = [];

  constructor(
    private httpService: HttpService,
    private readonly dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getAppointments();
  }

  getAppointments() {
    this.httpService.genericGet<Appointment[]>("appointments").subscribe((res: Appointment[]) => {
      this.appointments = res;
    })
  }

  handleApponintment() {
    this.httpService.genericGet<Patient[]>("patient").subscribe((res: Patient[]) => {
      this.patients = res;
      this.createAppointmentModal();
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

  handleCreatePatient() {
    const dialog = this.dialog.open(CreateModalComponent, {
      width: '476',
      height: '237px',
      data: {
        isPatient: true,
        isRoom: false,
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
      this.createAppointment(res.id);
    },
    (err) => {
      this.toastr.error('Algo deu errado ao cadastrar paciente.', 'Tente novamente');
    });
  }

  createAppointment(userId: number) {
    const body = {
      userId
    };
    this.httpService.genericPost("appointments", body).subscribe((res) => {
      this.toastr.success('Consulta iniciada com sucesso.', 'Sucesso');
      this.getAppointments();
    },
    (err) => {
      this.toastr.error('Algo deu errado ao iniciar consulta.', 'Tente novamente');
    });
  }

  createAppointmentModal() {
    const options = this.patients.map(p => p.cpf);
    const dialog = this.dialog.open(SelectModalComponent, {
      width: '476',
      height: '237px',
      data: {
        title: "Iniciar Atendimento",
        placeholder: "Selecione um paciente",
        options
      }
    });

    dialog.afterClosed().subscribe((res: string) => {
      if(res) {
        const patient = this.patients.find(p => p.cpf === res);
        if(!patient) {
          this.toastr.error('Paciente não encontrado.', 'Tente novamente');
          return;
        }
        this.createAppointment(patient.id);
      }
    })
  }

}
