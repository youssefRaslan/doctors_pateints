import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Message, SendMessageRequest } from '../models/chat.models';

@Injectable({ providedIn: 'root' })
export class ChatService {
  private readonly http = inject(HttpClient);
  private readonly chatUrl = `${environment.apiBaseUrl}/chat`;

  sendMessage(payload: SendMessageRequest): Observable<unknown> {
    return this.http.post<unknown>(`${this.chatUrl}/send-message`, payload);
  }

  getMessages(doctorId: number, patientId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.chatUrl}/${doctorId}/${patientId}`);
  }
}
