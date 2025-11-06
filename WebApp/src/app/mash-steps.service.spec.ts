import { TestBed } from '@angular/core/testing';

import { MashStepsService } from './mash-steps.service';

describe('MashStepsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MashStepsService = TestBed.get(MashStepsService);
    expect(service).toBeTruthy();
  });
});
