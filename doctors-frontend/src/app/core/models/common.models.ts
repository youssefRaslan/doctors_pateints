export interface ApiMessage {
  message: string;
}

export interface PagedResponse<T> {
  items?: T[];
  data?: T[];
  page?: number;
  pageSize?: number;
  totalCount?: number;
  [key: string]: unknown;
}
