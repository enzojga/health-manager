import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigationComponent } from "./containers/navigation/navigation.component";

@Component({
  selector: 'app-external',
  imports: [RouterOutlet, NavigationComponent],
  template: `
    <app-navigation>
      <router-outlet></router-outlet>
    </app-navigation>
  `,
})
export class ExternalComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
