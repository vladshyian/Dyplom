import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KafedraInfoComponent } from './kafedra-info.component';

describe('KafedraInfoComponent', () => {
  let component: KafedraInfoComponent;
  let fixture: ComponentFixture<KafedraInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KafedraInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KafedraInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
