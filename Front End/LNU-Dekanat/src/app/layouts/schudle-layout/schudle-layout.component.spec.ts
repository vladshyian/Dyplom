import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchudleLayoutComponent } from './schudle-layout.component';

describe('SchudleLayoutComponent', () => {
  let component: SchudleLayoutComponent;
  let fixture: ComponentFixture<SchudleLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SchudleLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SchudleLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
