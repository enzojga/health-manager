import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ButtonsCard } from '../../external/interfaces/external-interfaces';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-person-card',
  templateUrl: './person-card.component.html',
  styleUrls: ['./person-card.component.css'],
  imports: [
    CommonModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule
  ]
})
export class PersonCardComponent implements OnInit {

  @Input() name = '';
  @Input() ocupation = '';
  @Input() status: "Atendendo" | "Aguardando" | "Triagem" | "Check-in" | "Internado" = 'Aguardando';
  @Input() appointmentStarted: string | null = null;
  @Input() icons: ButtonsCard[] = [];
  @Output() action = new EventEmitter<string>();

  statusDictionary = {
    "Atendendo":  {
      color: "#FF2E1F",
      background: "#FDF1ED"
    },
    "Aguardando": {
      color: "#CB9F01",
      background: "#FFF8EB"
    },
    "Triagem": {
      color: "#1F52AB",
      background: "#E0E9F6"
    },
    "Check-in": {
      color: "#00662E",
      background: "#EBFFF4"
    },
    "Internado": {
      color: "#E5DAFB",
      background: "#4A13B9"
    },
  };

  constructor() { }

  ngOnInit() {
  }

  handleAction(action: string) {
    this.action.emit(action);
  }
}
