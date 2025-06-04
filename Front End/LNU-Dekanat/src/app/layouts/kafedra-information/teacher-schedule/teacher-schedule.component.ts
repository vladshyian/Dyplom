import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-teacher-schedule',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './teacher-schedule.component.html',
  styleUrl: './teacher-schedule.component.css'
})
export class TeacherScheduleComponent {
  @Input() teachers: any[] = [];
  semesters = ['1-й', '2-й'];
  selectedSemester = this.semesters[0];
  defaultTeacher = this.teachers[0];
  selectedTeacherId: string = '';
  
  constructor(private router: Router, private route: ActivatedRoute) {}

  onSemesterChange() {
    console.log(this.selectedSemester);
  }

  onTeacherChange() {
  console.log(this.selectedTeacherId);
  }

  viewSchedule(teacherId: string) {
    this.router.navigate(
      ['TeacherSchedule', teacherId],);
  }
}
