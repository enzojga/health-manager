import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { RouterModule, Router, NavigationEnd } from '@angular/router'; // Import Router and NavigationEnd
import { filter } from 'rxjs/operators'; // Import filter operator

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css'],
  imports: [MatIconModule, RouterModule, CommonModule],
  providers:[ MatIconRegistry ],
})
export class SideNavComponent implements OnInit {
  currentUrl = "";

  constructor(private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.currentUrl = event.urlAfterRedirects;
    });
  }

  ngOnInit() {
  }

}
