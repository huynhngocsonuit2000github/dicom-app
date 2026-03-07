import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../services/auth';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class NavbarComponent implements OnInit {


  isAuthenticated$: Observable<boolean> = new Observable<boolean>();

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.isAuthenticated$ = this.authService.isAuthenticated();
  }
}
