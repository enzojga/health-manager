import { Routes } from '@angular/router';
import { ExternalComponent } from './external/external.component';
import { externalRoutes } from './external/external.routing';

export const routes: Routes = [
    {
        path: '',
        component: ExternalComponent,
        children: externalRoutes
    },
    {
        path: '**',
        redirectTo: ''
    }
];
