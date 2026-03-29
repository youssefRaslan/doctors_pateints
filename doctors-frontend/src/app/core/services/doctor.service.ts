import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AddDoctorRequest,
  Doctor,
  DoctorByGmailResponse,
  GetDoctorByGmailRequest,
  UpdateDoctorRequest
} from '../models/doctor.models';

@Injectable({ providedIn: 'root' })
export class DoctorService {
  private readonly http = inject(HttpClient);
  private readonly doctorUrl = `${environment.apiBaseUrl}/Doctor`;

  getAllDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.doctorUrl}/v1/getalldoctors`);
  }

  addDoctor(payload: AddDoctorRequest): Observable<unknown> {
    const formData = new FormData();
    formData.append('name', payload.name);
    formData.append('specialization', payload.specialization);
    formData.append('email', payload.email);
    formData.append('phoneNumber', payload.phoneNumber);

    if (payload.address) {
      formData.append('address', payload.address);
    }

    if (payload.imageFile) {
      formData.append('imageFile', payload.imageFile);
    }

    return this.http.post<unknown>(`${this.doctorUrl}/v1/adddoctor`, formData);
  }

  getDoctorByGmail(payload: GetDoctorByGmailRequest): Observable<DoctorByGmailResponse> {
    return this.http.post<DoctorByGmailResponse>(`${this.doctorUrl}/v1/getgmaildoctor`, payload);
  }

  removeDoctor(payload: GetDoctorByGmailRequest): Observable<unknown> {
    return this.http.delete<unknown>(`${this.doctorUrl}/v1/removedoctor`, { body: payload });
  }

  updateDoctor(id: number, payload: UpdateDoctorRequest): Observable<unknown> {
    return this.http.patch<unknown>(`${this.doctorUrl}/v1/updatedoctor${id}`, payload);
  }
}
