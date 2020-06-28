import { TestBed, inject } from '@angular/core/testing';

import { BaseApiService } from './base-api-service.service';

describe('BaseApiServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BaseApiService]
    });
  });

  it('should be created', inject([BaseApiService], (service: BaseApiService) => {
    expect(service).toBeTruthy();
  }));
});
