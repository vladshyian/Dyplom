import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavantaComponent } from './navanta.component';

describe('NavantaComponent', () => {
  let component: NavantaComponent;
  let fixture: ComponentFixture<NavantaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavantaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavantaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
