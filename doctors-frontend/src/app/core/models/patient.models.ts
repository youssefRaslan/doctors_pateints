export interface Patient {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  address?: string;
  imageUrl?: string;
  birthDate?: string;
  age?: number;
}

export interface UpdatePatientRequest {
  id: number;
  name: string;
  phoneNumber: string;
  address?: string;
  birthDate?: string;
}
