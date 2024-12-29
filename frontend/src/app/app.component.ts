import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PatientsComponent } from './external/views/patients/patients.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';
}
