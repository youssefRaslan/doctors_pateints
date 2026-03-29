import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Patient, UpdatePatientRequest } from '../models/patient.models';

@Injectable({ providedIn: 'root' })
export class PatientService {
  private readonly http = inject(HttpClient);
  private readonly patientUrl = `${environment.apiBaseUrl}/patient`;

  getById(id: number): Observable<Patient> {
    return this.http.get<Patient>(`${this.patientUrl}/${id}`);
  }

  getAll(page = 1, pageSize = 10): Observable<Patient[]> {
    const params = new HttpParams().set('page', page).set('pageSize', pageSize);
    return this.http.get<Patient[]>(this.patientUrl, { params });
  }

  update(payload: UpdatePatientRequest): Observable<unknown> {
    return this.http.put<unknown>(`${this.patientUrl}/update`, payload);
  }

  delete(id: number): Observable<unknown> {
    return this.http.delete<unknown>(`${this.patientUrl}/delete/${id}`);
  }
}
