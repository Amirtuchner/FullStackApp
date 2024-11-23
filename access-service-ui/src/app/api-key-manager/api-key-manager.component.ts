import { Component } from '@angular/core';
import { ApiKeyService } from '../api-key.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule

@Component({
  selector: 'app-api-key-manager',
  standalone: true,
  imports: [CommonModule, FormsModule], // Add CommonModule here
  templateUrl: './api-key-manager.component.html',
  styleUrls: ['./api-key-manager.component.scss']
})
export class ApiKeyManagerComponent {
  userId = '';
  permissions = '';
  apiKey = '';
  tokens: any[] = [];

  constructor(private apiKeyService: ApiKeyService) {}

  createApiKey() {
    this.apiKeyService.createApiKey(this.userId, this.permissions.split(','))
      .subscribe(response => alert(`API Key: ${response.apiKey}`));
  }

  authenticate() {
    this.apiKeyService.authenticate(this.apiKey).subscribe(response => {
      alert(`JWT Token: ${response.token}`);
    });
  }

  getApiKeys() {
    this.apiKeyService.getApiKeys(this.userId).subscribe(tokens => {
      this.tokens = tokens;
    });
  }

  revokeApiKey(apiKey: string) {
    this.apiKeyService.revokeApiKey(apiKey).subscribe(() => {
      alert('API Key Revoked');
      this.getApiKeys();
    });
  }
}
