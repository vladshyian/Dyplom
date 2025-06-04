import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KafedrasMenuComponent } from './kafedras-menu.component';

describe('KafedrasMenuComponent', () => {
  let component: KafedrasMenuComponent;
  let fixture: ComponentFixture<KafedrasMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KafedrasMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KafedrasMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
