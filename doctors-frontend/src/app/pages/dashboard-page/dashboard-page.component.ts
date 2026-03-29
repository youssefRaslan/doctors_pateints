import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Doctor } from '../../core/models/doctor.models';
import { Message } from '../../core/models/chat.models';
import { Measurement } from '../../core/models/measurement.models';
import { Patient } from '../../core/models/patient.models';
import { ChatService } from '../../core/services/chat.service';
import { DoctorService } from '../../core/services/doctor.service';
import { MeasurementService } from '../../core/services/measurement.service';
import { PatientService } from '../../core/services/patient.service';
import { RelationshipService } from '../../core/services/relationship.service';
import { StorageService } from '../../core/services/storage.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-dashboard-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.scss'
})
export class DashboardPageComponent implements OnInit {
  private readonly doctorService = inject(DoctorService);
  private readonly patientService = inject(PatientService);
  private readonly measurementService = inject(MeasurementService);
  private readonly chatService = inject(ChatService);
  private readonly relationshipService = inject(RelationshipService);
  private readonly storage = inject(StorageService);
  private readonly auth = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);

  doctors: Doctor[] = [];
  patients: Patient[] = [];
  messages: Message[] = [];
  measurements: Measurement[] = [];

  doctorsError = '';
  protectedError = '';
  actionMessage = '';

  isLoadingDoctors = false;
  isLoadingProtectedData = false;

  readonly measurementForm = this.fb.nonNullable.group({
    patientId: [0, [Validators.required, Validators.min(1)]],
    sugarLevel: [0, [Validators.required, Validators.min(1)]],
    bloodPressure: [0, [Validators.required, Validators.min(1)]],
    date: [new Date().toISOString().slice(0, 10), [Validators.required]]
  });

  readonly chatForm = this.fb.nonNullable.group({
    doctorId: [0, [Validators.required, Validators.min(1)]],
    patientId: [0, [Validators.required, Validators.min(1)]],
    senderId: [0, [Validators.required, Validators.min(1)]],
    receiverId: [0, [Validators.required, Validators.min(1)]],
    content: ['']
  });

  readonly relationshipForm = this.fb.nonNullable.group({
    doctorId: [0, [Validators.required, Validators.min(1)]],
    patientEmail: ['', [Validators.required, Validators.email]],
    patientPhone: ['', [Validators.required, Validators.minLength(8)]]
  });

  get currentUserName(): string {
    return (this.storage.getUser()?.name as string) || 'Clinician';
  }

  ngOnInit(): void {
    this.loadDoctors();
    this.loadProtectedCollections();
  }

  loadDoctors(): void {
    this.doctorsError = '';
    this.isLoadingDoctors = true;

    this.doctorService.getAllDoctors().subscribe({
      next: (data) => {
        this.doctors = data;
        this.isLoadingDoctors = false;
      },
      error: (error: { error?: { message?: string } }) => {
        this.isLoadingDoctors = false;
        this.doctorsError = error.error?.message || 'Failed to load doctors list.';
      }
    });
  }

  loadProtectedCollections(): void {
    this.protectedError = '';
    this.isLoadingProtectedData = true;

    this.patientService.getAll(1, 20).subscribe({
      next: (data) => {
        this.patients = data;
        this.isLoadingProtectedData = false;
      },
      error: (error: { error?: { message?: string } }) => {
        this.isLoadingProtectedData = false;
        this.protectedError =
          error.error?.message ||
          'Could not load protected endpoints. Login token may be missing or expired.';
      }
    });
  }

  submitMeasurement(): void {
    if (this.measurementForm.invalid) {
      this.measurementForm.markAllAsTouched();
      return;
    }

    this.actionMessage = '';
    const value = this.measurementForm.getRawValue();

    this.measurementService
      .addMeasurement({
        patientId: Number(value.patientId),
        sugarLevel: Number(value.sugarLevel),
        bloodPressure: Number(value.bloodPressure),
        date: new Date(value.date).toISOString()
      })
      .subscribe({
        next: () => {
          this.actionMessage = 'Measurement added successfully.';
          this.loadMeasurements();
        },
        error: (error: { error?: { message?: string } }) => {
          this.actionMessage = error.error?.message || 'Failed to add measurement.';
        }
      });
  }

  loadMeasurements(): void {
    const patientId = Number(this.measurementForm.getRawValue().patientId);
    if (!patientId) {
      return;
    }

    this.measurementService.getPatientMeasurements(patientId).subscribe({
      next: (items) => {
        this.measurements = items;
      },
      error: (error: { error?: { message?: string } }) => {
        this.actionMessage = error.error?.message || 'Failed to load measurements.';
      }
    });
  }

  submitMessage(): void {
    if (this.chatForm.invalid) {
      this.chatForm.markAllAsTouched();
      return;
    }

    this.actionMessage = '';
    const value = this.chatForm.getRawValue();

    this.chatService
      .sendMessage({
        senderId: Number(value.senderId),
        receiverId: Number(value.receiverId),
        content: value.content.trim()
      })
      .subscribe({
        next: () => {
          this.actionMessage = 'Message sent successfully.';
          this.loadMessages();
        },
        error: (error: { error?: { message?: string } }) => {
          this.actionMessage = error.error?.message || 'Failed to send message.';
        }
      });
  }

  loadMessages(): void {
    const value = this.chatForm.getRawValue();
    if (!value.doctorId || !value.patientId) {
      return;
    }

    this.chatService.getMessages(Number(value.doctorId), Number(value.patientId)).subscribe({
      next: (items) => {
        this.messages = items;
      },
      error: (error: { error?: { message?: string } }) => {
        this.actionMessage = error.error?.message || 'Failed to load messages.';
      }
    });
  }

  submitRelationshipRequest(): void {
    if (this.relationshipForm.invalid) {
      this.relationshipForm.markAllAsTouched();
      return;
    }

    this.actionMessage = '';

    this.relationshipService.sendRequest(this.relationshipForm.getRawValue()).subscribe({
      next: () => {
        this.actionMessage = 'Relationship request sent successfully.';
      },
      error: (error: { error?: { message?: string } }) => {
        this.actionMessage = error.error?.message || 'Failed to send relationship request.';
      }
    });
  }

  logout(): void {
    this.auth.logout();
    void this.router.navigate(['/login']);
  }
}
