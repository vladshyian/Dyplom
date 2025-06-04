import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KafedraComponent } from './kafedra.component';

describe('KafedraComponent', () => {
  let component: KafedraComponent;
  let fixture: ComponentFixture<KafedraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KafedraComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KafedraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
