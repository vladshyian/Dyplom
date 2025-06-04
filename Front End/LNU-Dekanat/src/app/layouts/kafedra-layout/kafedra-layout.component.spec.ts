import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KafedraLayoutComponent } from './kafedra-layout.component';

describe('KafedraLayoutComponent', () => {
  let component: KafedraLayoutComponent;
  let fixture: ComponentFixture<KafedraLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KafedraLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KafedraLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
