import { CommonModule } from '@angular/common';
import { AuthService } from './../../services/auth';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  email = '';
  password = '';
  error = '';

  constructor(private router: Router, private authService: AuthService, private cdr: ChangeDetectorRef) { }

  login() {

    this.authService.login(this.email, this.password).subscribe({
      next: token => {
        this.router.navigateByUrl('/todos');
        console.log('The token is ' + token);
      },
      error: err => {
        this.error = 'Load failed';
        this.cdr.detectChanges();
      }
    }
    )
  }
}
