import { TestBed } from '@angular/core/testing';

import { BoilingPlate1Service } from './boiling-plate1.service';

describe('BoilingPlate1Service', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BoilingPlate1Service = TestBed.get(BoilingPlate1Service);
    expect(service).toBeTruthy();
  });
});
