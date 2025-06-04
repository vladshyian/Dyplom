import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutKafedraComponent } from './about-kafedra.component';

describe('AboutKafedraComponent', () => {
  let component: AboutKafedraComponent;
  let fixture: ComponentFixture<AboutKafedraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AboutKafedraComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutKafedraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
