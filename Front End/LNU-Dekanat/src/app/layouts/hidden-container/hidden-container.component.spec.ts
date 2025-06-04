import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HiddenContainerComponent } from './hidden-container.component';

describe('HiddenContainerComponent', () => {
  let component: HiddenContainerComponent;
  let fixture: ComponentFixture<HiddenContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HiddenContainerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HiddenContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
