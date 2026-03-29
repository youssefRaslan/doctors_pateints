import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { RegisterUserRequest } from '../../core/models/auth.models';

@Component({
  selector: 'app-login-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly authForm = this.fb.nonNullable.group({
    name: [''],
    phone: [''],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  selectedRole: 'doctor' | 'patient' = 'doctor';
  selectedMode: 'login' | 'register' = 'login';
  selectedPhoto?: File;

  isSubmitting = false;
  errorMessage = '';
  successMessage = '';

  setRole(role: 'doctor' | 'patient'): void {
    this.selectedRole = role;
    this.errorMessage = '';
    this.successMessage = '';
  }

  setMode(mode: 'login' | 'register'): void {
    this.selectedMode = mode;
    this.errorMessage = '';
    this.successMessage = '';
    this.selectedPhoto = undefined;
    this.applyModeValidators();
  }

  onPhotoSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedPhoto = input.files?.[0] ?? undefined;
  }

  submit(): void {
    this.applyModeValidators();
    if (this.authForm.invalid || this.isSubmitting) {
      this.authForm.markAllAsTouched();
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';
    this.isSubmitting = true;

    const formValue = this.authForm.getRawValue();

    if (this.selectedMode === 'login') {
      this.authService.login({ email: formValue.email, password: formValue.password }).subscribe({
        next: (response) => {
          this.isSubmitting = false;

          if (!response.success) {
            this.errorMessage = response.message || 'Login failed.';
            return;
          }

          void this.router.navigate(['/dashboard']);
        },
        error: (error: { error?: { message?: string } }) => {
          this.isSubmitting = false;
          this.errorMessage = error.error?.message || 'Login failed. Check your credentials.';
        }
      });
      return;
    }

    const payload: RegisterUserRequest = {
      name: formValue.name,
      email: formValue.email,
      password: formValue.password,
      phone: formValue.phone,
      photo: this.selectedPhoto
    };

    const request$ =
      this.selectedRole === 'doctor'
        ? this.authService.registerDoctor(payload)
        : this.authService.registerPatient(payload);

    request$.subscribe({
      next: (response) => {
        this.isSubmitting = false;

        if (!response.success) {
          this.errorMessage = response.message || 'Registration failed.';
          return;
        }

        this.successMessage =
          this.selectedRole === 'doctor'
            ? 'Registered successfully. Verify your doctor email, then login.'
            : 'Registered successfully. You can login now.';

        this.setMode('login');
        this.authForm.patchValue({ email: formValue.email, password: '' });
      },
      error: (error: { error?: { message?: string } }) => {
        this.isSubmitting = false;
        this.errorMessage = error.error?.message || 'Registration failed. Please try again.';
      }
    });
  }

  private applyModeValidators(): void {
    const nameControl = this.authForm.controls.name;
    const phoneControl = this.authForm.controls.phone;

    if (this.selectedMode === 'register') {
      nameControl.setValidators([Validators.required, Validators.minLength(3)]);
      phoneControl.setValidators([Validators.required, Validators.minLength(8)]);
    } else {
      nameControl.clearValidators();
      phoneControl.clearValidators();
    }

    nameControl.updateValueAndValidity({ emitEvent: false });
    phoneControl.updateValueAndValidity({ emitEvent: false });
  }
}
