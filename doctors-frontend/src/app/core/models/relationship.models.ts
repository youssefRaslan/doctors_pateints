import { Patient } from './patient.models';

export interface SendRequestPayload {
  doctorId: number;
  patientEmail: string;
  patientPhone: string;
}

export interface DoctorRequest {
  requestId: number;
  doctorId: number;
  doctorName: string;
  patientId: number;
  patientName: string;
  status: string;
  createdAt: string;
}

export type AcceptedPatient = Patient;
