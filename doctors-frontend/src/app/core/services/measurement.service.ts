import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AddMeasurementRequest, Measurement } from '../models/measurement.models';

@Injectable({ providedIn: 'root' })
export class MeasurementService {
  private readonly http = inject(HttpClient);
  private readonly measurementUrl = `${environment.apiBaseUrl}/measurement`;

  addMeasurement(payload: AddMeasurementRequest): Observable<unknown> {
    return this.http.post<unknown>(`${this.measurementUrl}/add`, payload);
  }

  getPatientMeasurements(patientId: number): Observable<Measurement[]> {
    return this.http.get<Measurement[]>(`${this.measurementUrl}/${patientId}`);
  }
}
