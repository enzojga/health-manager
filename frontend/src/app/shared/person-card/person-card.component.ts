import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-person-card',
  templateUrl: './person-card.component.html',
  styleUrls: ['./person-card.component.css'],
  imports: [CommonModule]
})
export class PersonCardComponent implements OnInit {

  @Input() name = '';
  @Input() ocupation = '';
  @Input() status: "Trabalhando" | "Aguardando" | "Triagem" | "Check-in" | "Internado" = 'Aguardando';
  @Input() appointmentStarted: string | null = null;


  statusDictionary = {
    "Trabalhando":  {
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

}
