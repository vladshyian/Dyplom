import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkersOnKafedraComponent } from './workers-on-kafedra.component';

describe('WorkersOnKafedraComponent', () => {
  let component: WorkersOnKafedraComponent;
  let fixture: ComponentFixture<WorkersOnKafedraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkersOnKafedraComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkersOnKafedraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
