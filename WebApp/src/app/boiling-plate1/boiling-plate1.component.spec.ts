import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoilingPlate1Component } from './boiling-plate1.component';

describe('BoilingPlate1Component', () => {
  let component: BoilingPlate1Component;
  let fixture: ComponentFixture<BoilingPlate1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoilingPlate1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoilingPlate1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
