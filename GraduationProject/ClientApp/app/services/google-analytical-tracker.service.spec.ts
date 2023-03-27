import { TestBed } from '@angular/core/testing';

import { GoogleAnalyticalTrackerService } from './google-analytical-tracker.service';

describe('GoogleAnalyticalTrackerService', () => {
  let service: GoogleAnalyticalTrackerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GoogleAnalyticalTrackerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
