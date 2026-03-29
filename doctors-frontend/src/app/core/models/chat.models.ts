export interface SendMessageRequest {
  senderId: number;
  receiverId: number;
  content: string;
  fileUrl?: string;
}

export interface Message {
  id: number;
  senderId: number;
  receiverId: number;
  content: string;
  fileUrl?: string;
  createdAt: string;
}
