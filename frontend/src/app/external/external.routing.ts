import { Routes } from '@angular/router';
import { PatientsComponent } from './views/patients/patients.component';
import { NursesComponent } from './views/nurses/nurses.component';
import { CheckInComponent } from './views/check-in/check-in.component';
import { RoomsComponent } from './views/rooms/rooms.component';

export const externalRoutes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: PatientsComponent },
      { path: 'triagem', component: NursesComponent },
      { path: 'check-in', component: CheckInComponent },
      { path: 'enfermaria', component: RoomsComponent },
    ]
  }
];
