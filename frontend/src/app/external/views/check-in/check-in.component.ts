import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from '../../../shared/person-card/person-card.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { HttpService } from '../../../services/http.service';
import { PatientDTO, Workers } from '../../interfaces/external-interfaces';
import { CreateModalComponent } from '../../../shared/create-modal/create-modal.component';

@Component({
  selector: 'app-check-in',
  templateUrl: './check-in.component.html',
  styleUrls: ['./check-in.component.css'],
  imports: [
    PersonCardComponent,
    MatIconModule,
    MatMenuModule,
    MatTooltipModule,
    MatButtonModule
  ],
})
export class CheckInComponent implements OnInit {

  doctors: Workers[] = [];

  constructor(
    private httpService: HttpService,
    private readonly dialog: MatDialog,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.getDoctors();
  }

  getDoctors() {
    this.httpService.genericGet<Workers[]>("worker?type=doctor").subscribe((res: Workers[]) => {
      this.doctors = res;
    })
  }
  
  handleCreateDoctor() {
    const dialog = this.dialog.open(CreateModalComponent, {
      width: '476px',
      height: '237px',
      data: {
        isPatient: false,
        isRoom: false,
        title: "Cadastrar médico"
      }
    });

    dialog.afterClosed().subscribe((res: PatientDTO) => {
      if(res) {
        this.createDoctor(res.name);
      }
    })
  }

  createDoctor(name: string) {
    const body = {
      name,
      type: "Doctor"
    }
    this.httpService.genericPost<Workers>("worker", body).subscribe((res: Workers) => {
      this.toastr.success('Médico cadastrado com sucesso.', 'Sucesso');
      this.getDoctors();
    },
    (err) => {
      this.toastr.error('Algo deu errado ao cadastrar médico.', 'Tente novamente');
    });
  }
}
