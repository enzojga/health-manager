import { Routes } from '@angular/router';
import { PatientsComponent } from './views/patients/patients.component';
import { NursesComponent } from './views/nurses/nurses.component';

export const externalRoutes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: PatientsComponent },
      { path: 'enfermaria', component: NursesComponent },
    ]
  }
];
