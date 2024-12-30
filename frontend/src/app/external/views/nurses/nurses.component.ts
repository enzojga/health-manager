import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from '../../../shared/person-card/person-card.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { HttpService } from '../../../services/http.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { PatientDTO, Workers } from '../../interfaces/external-interfaces';
import { CreateModalComponent } from '../../../shared/create-modal/create-modal.component';

@Component({
  selector: 'app-nurses',
  templateUrl: './nurses.component.html',
  styleUrls: ['./nurses.component.css'],
  imports: [
    PersonCardComponent,
    MatIconModule,
    MatMenuModule,
    MatTooltipModule,
    MatButtonModule
  ],
  
})
export class NursesComponent implements OnInit {

  nurses: Workers[] = [];

  constructor(
    private httpService: HttpService,
    private readonly dialog: MatDialog,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.getNurses();
  }

  getNurses() {
    this.httpService.genericGet<Workers[]>("worker?type=nurse").subscribe((res: Workers[]) => {
      this.nurses = res;
    })
  }
  
  handleCreateNurse() {
    const dialog = this.dialog.open(CreateModalComponent, {
      width: '476px',
      height: '237px',
      data: {
        isPatient: false,
        isRoom: false,
        title: "Cadastrar enfermeiro"
      }
    });

    dialog.afterClosed().subscribe((res: PatientDTO) => {
      if(res) {
        this.createNurse(res.name);
      }
    })
  }

  createNurse(name: string) {
    const body = {
      name,
      type: "Nurse"
    }
    this.httpService.genericPost<Workers>("worker", body).subscribe((res: Workers) => {
      this.toastr.success('Enfermeiro cadastrado com sucesso.', 'Sucesso');
      this.getNurses();
    },
    (err) => {
      this.toastr.error('Algo deu errado ao cadastrar enfermeiro.', 'Tente novamente');
    });
  }
}
