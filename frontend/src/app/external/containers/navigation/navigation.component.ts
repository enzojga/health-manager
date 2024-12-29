import { Component, OnInit } from '@angular/core';
import { SideNavComponent } from "../side-nav/side-nav.component";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
  imports: [SideNavComponent]
})
export class NavigationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
