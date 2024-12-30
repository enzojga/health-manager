import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from "../../../shared/person-card/person-card.component";
import { HttpService } from '../../../services/http.service';
import { Appointment, ButtonsCard, Patient, PatientDTO, Room, Workers } from '../../interfaces/external-interfaces';
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

  getIcons(patient: Patient) {
    const icons: ButtonsCard[] = [];
    if(patient.roomId) {
      icons.push({icon: "logout", tooltip: "Liberar paciente"});
    }
    if(patient.doctorId) {
      icons.push({icon: "logout", tooltip: "Liberar paciente"});
      icons.push({icon: "home_health", tooltip: "Internar paciente"});
    }
    if(patient.nurseId) {
      icons.push({icon: "healing", tooltip: "Enviar para o check-in"});
    }
    const awaitng = !patient.roomId && !patient.doctorId && !patient.nurseId;
    if(awaitng) {
      icons.push({icon: "emergency", tooltip: "Iniciar atendimento"});
    }
    return icons;
  }

  handleCreatePatient() {
    const dialog = this.dialog.open(CreateModalComponent, {
      width: '476px',
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

  handleAction(event: string, item: Appointment) {
    let stage = '';
    let url = '';
    let param = '';
    if(event === 'emergency') {
      url = 'worker?type=nurse&available=true';
      stage = 'enfermeiro';
      param = 'nurse';
    }
    if(event === 'healing') {
      url = 'worker?type=doctor&available=true';
      stage = 'médico';
      param = 'doctor';
    }
    if(event === 'home_health') {
      stage = 'quarto';
      param = 'room';
      url = 'room';
    }
    if(event === 'logout') {
      this.finishAppointment(item);
      return;
    }
    this.httpService.genericGet<Workers[] | Room[]>(url).subscribe((res: Workers[] | Room[]) => {
      this.handleGenericSelect(stage, res, item.patient.id, param);
    })
    
  }

  handleGenericSelect(stage: string, reqResponse: Workers[] | Room[], patientId: number, param: string) {
    let options: string[] = [];
    if(param === 'room') {
      options = (reqResponse as Room[]).map(r => r.id.toString());
    } else {
      options = (reqResponse as Workers[]).map(w => w.name);
    }
    if(options.length === 0) {
      this.toastr.error(`Sem ${stage}s disponiveis. Tente novamente mais tarde`, 'Tente novamente');
      return;
    }
    const dialog = this.dialog.open(SelectModalComponent, {
      height: '237px',
      data: {
        title: `Selecione um ${stage}`,
        placeholder: "Selecione um paciente",
        options
      }
    });

    dialog.afterClosed().subscribe((res: string) => {
      if(res) {
        let id = null;
        if(param === 'room') {
          id = reqResponse.find(r => r.id === Number(res));
        } else {
          id = (reqResponse as Workers[]).find(w => w.name === res);
        }
        if(!id) {
          this.toastr.error('Algo deu errado, por favor, tente novamente.', 'Tente novamente');
          return;
        }
        const url = `Patient/${patientId}/associate-${param}/${id.id}`;
        this.vinclutePatient(url);
      }
    });
  }

  vinclutePatient(url: string) {
    this.httpService.genericPut(url, {}).subscribe((res) => {
      console.log(res);
      this.toastr.success('Paciente vinculado com sucesso.', 'Sucesso');
      this.getAppointments();
    });
  }

  finishAppointment(item: Appointment) {
    this.httpService.genericDelete(`Patient/${item.patient.id}/finish-appointment`).subscribe((res) => {
      this.toastr.success('Consulta finalizada com sucesso.', 'Sucesso');
      this.getAppointments();
    });
  }
}
