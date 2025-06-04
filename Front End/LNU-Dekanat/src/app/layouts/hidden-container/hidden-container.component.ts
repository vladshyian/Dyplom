import { animate, state, style, transition, trigger } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-hidden-container',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hidden-container.component.html',
  styleUrls: ['./hidden-container.component.css'], 
  animations: [
    trigger('fadeAnimation', [
      state('void', style({ opacity: 0 })),
      transition(':enter', [
        style({ opacity: 0 }),
        animate('1s ease-in', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        animate('500ms ease-out', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class HiddenContainerComponent {
  @Input() buttonText: string = 'Click';
  @Input() infoText: string = 'Initial info text';
  isShowed = false;

  click() {
    this.isShowed = !this.isShowed;
  }
}
