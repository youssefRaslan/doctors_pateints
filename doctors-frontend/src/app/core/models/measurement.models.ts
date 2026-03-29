export interface AddMeasurementRequest {
  patientId: number;
  sugarLevel: number;
  bloodPressure: number;
  date: string;
}

export interface Measurement {
  id: number;
  patientId: number;
  sugarLevel: number;
  bloodPressure: number;
  date: string;
}
