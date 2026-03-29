export interface LoginRequest {
  email: string;
  password: string;
}

export interface VerifyEmailRequest {
  email: string;
  verificationCode: string;
}

export interface RegisterUserRequest {
  name: string;
  email: string;
  password: string;
  phone: string;
  photo?: File;
}

export interface AuthUser {
  id?: number;
  name?: string;
  email?: string;
  role?: string;
  [key: string]: unknown;
}

export interface AuthResponse {
  success: boolean;
  message: string;
  token?: string;
  user?: AuthUser;
}
