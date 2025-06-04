import { Component, Input } from '@angular/core';
import { DepartamentTeachets } from '../../../models/departament/departamentTeachers.module';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-workers-on-kafedra',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './workers-on-kafedra.component.html',
  styleUrl: './workers-on-kafedra.component.css'
})
export class WorkersOnKafedraComponent {

  @Input() teachers:  DepartamentTeachets[] = [];

}
