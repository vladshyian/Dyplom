import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-about-kafedra',
  standalone: true,
  imports: [],
  templateUrl: './about-kafedra.component.html',
  styleUrl: './about-kafedra.component.css'
})
export class AboutKafedraComponent {

  @Input() teacher_name: string = '';
  @Input() kafedra_phone: string = '';
  @Input() about_kafedra: string = '';
}
