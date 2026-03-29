import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';

export const routes: Routes = [
	{ path: 'login', component: LoginPageComponent },
	{ path: 'dashboard', component: DashboardPageComponent, canActivate: [authGuard] },
	{ path: '', pathMatch: 'full', redirectTo: 'login' },
	{ path: '**', redirectTo: 'login' }
];
