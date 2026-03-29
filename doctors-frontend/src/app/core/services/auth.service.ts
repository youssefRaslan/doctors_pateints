import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AuthResponse,
  LoginRequest,
  RegisterUserRequest,
  VerifyEmailRequest
} from '../models/auth.models';
import { StorageService } from './storage.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly storage = inject(StorageService);
  private readonly authUrl = `${environment.apiBaseUrl}/auth`;

  login(payload: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.authUrl}/login`, payload).pipe(
      tap((response) => {
        if (response.token) {
          this.storage.setToken(response.token);
        }

        if (response.user) {
          this.storage.setUser(response.user);
        }
      })
    );
  }

  registerDoctor(payload: RegisterUserRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.authUrl}/register/doctor`, this.toFormData(payload));
  }

  registerPatient(payload: RegisterUserRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.authUrl}/register/patient`, this.toFormData(payload));
  }

  verifyEmail(payload: VerifyEmailRequest, role: 'doctor' | 'patient'): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.authUrl}/verify-email?role=${role}`, payload);
  }

  logout(): void {
    this.storage.clear();
  }

  isAuthenticated(): boolean {
    return !!this.storage.getToken();
  }

  private toFormData(payload: RegisterUserRequest): FormData {
    const formData = new FormData();
    formData.append('name', payload.name);
    formData.append('email', payload.email);
    formData.append('password', payload.password);
    formData.append('phone', payload.phone);

    if (payload.photo) {
      formData.append('photo', payload.photo);
    }

    return formData;
  }
}
