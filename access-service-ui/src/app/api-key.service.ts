import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiKeyService {
  private apiBase = 'https://localhost:7232/api/ApiKeys';

  constructor(private http: HttpClient) {}

  createApiKey(userId: string, permissions: string[]): Observable<any> {
    return this.http.post(this.apiBase, { userId, permissions });
  }

  authenticate(apiKey: string): Observable<any> {
    return this.http.post(`${this.apiBase}/authenticate`, { apiKey });
  }

  revokeApiKey(apiKey: string): Observable<void> {
    return this.http.delete<void>(`${this.apiBase}/${apiKey}`);
  }

  getApiKeys(userId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiBase}?userId=${userId}`);
  }
}
