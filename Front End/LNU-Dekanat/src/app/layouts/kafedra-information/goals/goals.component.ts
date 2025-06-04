import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [],
  templateUrl: './goals.component.html',
  styleUrl: './goals.component.css'
})
export class GoalsComponent {

  @Input() mission: string = '';
  @Input() visia: string = '';
  @Input() strategic_goals: string = '';

}
