import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalUserListComponent } from './modal-user-list.component';

describe('ModalUserListComponent', () => {
  let component: ModalUserListComponent;
  let fixture: ComponentFixture<ModalUserListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalUserListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
