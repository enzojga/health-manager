import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigationComponent } from "./containers/navigation/navigation.component";
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';

@Component({
  selector: 'app-external',
  imports: [RouterOutlet, NavigationComponent],
  template: `
    <app-navigation>
      <router-outlet></router-outlet>
    </app-navigation>
  `
})
export class ExternalComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
