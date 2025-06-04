import { animate, state, style, transition, trigger } from '@angular/animations';
import { ChangeDetectorRef, Component, Input, QueryList, ViewChildren } from '@angular/core';

@Component({
  selector: 'app-scorecard-layout',
  standalone: true,
  imports: [],
  templateUrl: './scorecard-layout.component.html',
  styleUrl: './scorecard-layout.component.css',
  animations: [
    trigger('fadeAnimation', [
      state('void', style({ opacity: 0 })),
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.5s ease-in', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        animate('0.5s ease-out', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class ScorecardLayoutComponent {
  @Input() courses: { id: number; name: string; labs: number }[] = [];
  @Input() score: string = 'Initial info text';
  @Input() interval: string = 'Initial info text';
  @Input() numOfLabs: number = 0;

  isShowed: boolean[] = [];
  isModalOpen: boolean = false;

  ngOnInit() {
    this.isShowed = new Array(this.courses.length).fill(false);
  }

  constructor(private cdr: ChangeDetectorRef) {}

  counter(i: number) {
    return new Array(i);
  }

  click(index: number) {
    this.isShowed[index] = !this.isShowed[index];
    this.cdr.detectChanges();
  }

  openModal(labIndex: number): void {
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
  }

}
