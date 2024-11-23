import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiKeyManagerComponent } from './api-key-manager.component';

describe('ApiKeyManagerComponent', () => {
  let component: ApiKeyManagerComponent;
  let fixture: ComponentFixture<ApiKeyManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiKeyManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApiKeyManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
