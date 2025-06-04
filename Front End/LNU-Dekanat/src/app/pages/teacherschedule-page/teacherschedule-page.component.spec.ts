import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherschedulePageComponent } from './teacherschedule-page.component';

describe('TeacherschedulePageComponent', () => {
  let component: TeacherschedulePageComponent;
  let fixture: ComponentFixture<TeacherschedulePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TeacherschedulePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeacherschedulePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
