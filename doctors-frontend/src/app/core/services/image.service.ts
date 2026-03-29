import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UploadImageResponse } from '../models/image.models';

@Injectable({ providedIn: 'root' })
export class ImageService {
  private readonly http = inject(HttpClient);
  private readonly imageUrl = `${environment.apiBaseUrl}/Images`;

  uploadPhoto(file: File): Observable<UploadImageResponse> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<UploadImageResponse>(`${this.imageUrl}/uploadphoto`, formData);
  }

  uploadFile(file: File): Observable<UploadImageResponse> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<UploadImageResponse>(`${this.imageUrl}/uploadfile`, formData);
  }

  deletePhoto(publicId: string): Observable<unknown> {
    const params = new HttpParams().set('publicId', publicId);
    return this.http.delete<unknown>(`${this.imageUrl}/deletephoto`, { params });
  }
}
