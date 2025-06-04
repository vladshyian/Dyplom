import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KafedraMenuComponent } from './kafedra-menu.component';

describe('KafedraMenuComponent', () => {
  let component: KafedraMenuComponent;
  let fixture: ComponentFixture<KafedraMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KafedraMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KafedraMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
