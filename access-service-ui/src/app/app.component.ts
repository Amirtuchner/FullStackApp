import { Component } from '@angular/core';
import { ApiKeyManagerComponent } from './api-key-manager/api-key-manager.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ApiKeyManagerComponent],  // Keep only the necessary components
  template: `
    <app-api-key-manager></app-api-key-manager>
  `,
  styleUrls: ['./app.component.scss']
})
export class AppComponent {}
