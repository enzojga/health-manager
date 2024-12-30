import { Component, OnInit } from '@angular/core';
import { PersonCardComponent } from '../../../shared/person-card/person-card.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { Room, RoomDTO } from '../../interfaces/external-interfaces';
import { HttpService } from '../../../services/http.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CreateModalComponent } from '../../../shared/create-modal/create-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css'],
  imports: [
    PersonCardComponent,
    MatIconModule,
    MatMenuModule,
    MatTooltipModule,
    MatButtonModule,
    CommonModule
  ],
})
export class RoomsComponent implements OnInit {

  rooms: Room[] = [];

  constructor(
    private httpService: HttpService,
    private readonly dialog: MatDialog,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.getRooms();
  }

  getRooms() {
    this.httpService.genericGet<Room[]>("Room").subscribe((res: Room[]) => {
      this.rooms = res;
    });
  }
  
  handleCreateRoom() {
    const dialog = this.dialog.open(CreateModalComponent, {
      width: '476px',
      height: '237px',
      data: {
        isPatient: false,
        isRoom: true,
        title: "Cadastrar quarto"
      }
    });

    dialog.afterClosed().subscribe((res: RoomDTO) => {
      if(res) {
        this.createRoom(res.capacity);
      }
    });
  }

  createRoom(capacity: number) {
    const body = {
      capacity,
    }
    this.httpService.genericPost<Room>("room", body).subscribe((res: Room) => {
      this.toastr.success('Quarto cadastrado com sucesso.', 'Sucesso');
      this.getRooms();
    },
    (err) => {
      this.toastr.error('Algo deu errado ao cadastrar um quarto.', 'Tente novamente');
    });
  }
}
