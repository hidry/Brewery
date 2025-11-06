import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoilingPlate2Component } from './boiling-plate2.component';

describe('BoilingPlate2Component', () => {
  let component: BoilingPlate2Component;
  let fixture: ComponentFixture<BoilingPlate2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoilingPlate2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoilingPlate2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
