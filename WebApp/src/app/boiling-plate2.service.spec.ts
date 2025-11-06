import { TestBed } from '@angular/core/testing';

import { BoilingPlate2Service } from './boiling-plate2.service';

describe('BoilingPlate2Service', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BoilingPlate2Service = TestBed.get(BoilingPlate2Service);
    expect(service).toBeTruthy();
  });
});
