import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AuthService } from './services/auth';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
    constructor(private auth: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
        const requiredRole = route.data['role'] as string; // 1 role only (simple)
        console.log('check guard');

        return this.auth.loadRolesIfEmpty().pipe(
            map(() => {
                if (!requiredRole) return true;

                if (this.auth.hasRole(requiredRole)) return true;

                this.router.navigate(['/login']);
                return false;
            })
        );
    }
}
