export interface Doctor {
  id?: number;
  name: string;
  specialization: string;
  email: string;
  phoneNumber: string;
  imageFile?: string;
}

export interface AddDoctorRequest {
  name: string;
  specialization: string;
  email: string;
  phoneNumber: string;
  address?: string;
  imageFile?: File;
}

export interface GetDoctorByGmailRequest {
  gmail: string;
}

export interface DoctorByGmailResponse {
  id: number;
  name: string;
  imageFile: string;
}

export interface UpdateDoctorRequest {
  image: string;
  phoneNumber: string;
  specialization: string;
}
