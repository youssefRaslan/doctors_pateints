import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AcceptedPatient, DoctorRequest, SendRequestPayload } from '../models/relationship.models';

@Injectable({ providedIn: 'root' })
export class RelationshipService {
  private readonly http = inject(HttpClient);
  private readonly doctorBaseUrl = `${environment.apiBaseUrl}/doctor`;
  private readonly patientBaseUrl = `${environment.apiBaseUrl}/patient`;

  sendRequest(payload: SendRequestPayload): Observable<unknown> {
    return this.http.post<unknown>(`${this.doctorBaseUrl}/send-request`, payload);
  }

  getDoctorPatients(doctorId: number): Observable<AcceptedPatient[]> {
    const params = new HttpParams().set('doctorId', doctorId);
    return this.http.get<AcceptedPatient[]>(`${this.doctorBaseUrl}/patients`, { params });
  }

  getSentRequests(doctorId: number): Observable<DoctorRequest[]> {
    const params = new HttpParams().set('doctorId', doctorId);
    return this.http.get<DoctorRequest[]>(`${this.doctorBaseUrl}/sent-requests`, { params });
  }

  removePatient(doctorId: number, patientId: number): Observable<unknown> {
    const params = new HttpParams().set('doctorId', doctorId);
    return this.http.delete<unknown>(`${this.doctorBaseUrl}/remove-patient/${patientId}`, { params });
  }

  getIncomingRequests(patientId: number): Observable<DoctorRequest[]> {
    const params = new HttpParams().set('patientId', patientId);
    return this.http.get<DoctorRequest[]>(`${this.patientBaseUrl}/requests`, { params });
  }

  acceptRequest(patientId: number, requestId: number): Observable<unknown> {
    const params = new HttpParams().set('patientId', patientId);
    return this.http.post<unknown>(`${this.patientBaseUrl}/accept/${requestId}`, null, { params });
  }

  rejectRequest(patientId: number, requestId: number): Observable<unknown> {
    const params = new HttpParams().set('patientId', patientId);
    return this.http.post<unknown>(`${this.patientBaseUrl}/reject/${requestId}`, null, { params });
  }

  removeDoctor(patientId: number, doctorId: number): Observable<unknown> {
    const params = new HttpParams().set('patientId', patientId);
    return this.http.delete<unknown>(`${this.patientBaseUrl}/remove-doctor/${doctorId}`, { params });
  }
}
