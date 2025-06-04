import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScorecardLayoutComponent } from './scorecard-layout.component';

describe('ScorecardLayoutComponent', () => {
  let component: ScorecardLayoutComponent;
  let fixture: ComponentFixture<ScorecardLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScorecardLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScorecardLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
