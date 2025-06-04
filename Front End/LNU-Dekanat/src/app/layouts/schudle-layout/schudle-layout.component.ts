import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { IScheduleItem } from '../../models/schedule/schedule.module';
import { ScheduleService } from '../../core/services/schedule/schedule-service.service';

@Component({
  selector: 'app-schudle-layout',
  standalone: true,
  imports: [],
  templateUrl: './schudle-layout.component.html',
  styleUrl: './schudle-layout.component.css'
})
export class SchudleLayoutComponent implements OnChanges {

  @Input() schedule: {id: number; pairNumber: number; time: string; name: string; dayOfWeek: string }[] = [];

  userRole = localStorage.getItem("userRole");

  constructor(private scheduleService:ScheduleService) {}

  ngOnChanges(changes: SimpleChanges): void {
     if (changes['schedule']) {
    }
  }

  deleteSchedule(scheduleItem: IScheduleItem): void {

    scheduleItem.name = this.removeGroupName(scheduleItem.name);

    const confirmation = confirm('Ви впевнені, що хочете видалити цей розклад?');


    if (confirmation) {
      this.scheduleService.deleteSchedule(scheduleItem).subscribe({
        next: (response) =>
        {
          window.location.reload();
        },
        error: (error) =>
        {
          console.log(error)
        }
      })
    }
  }

  private removeGroupName(name: string): string {
    if (!name.includes('<br>')) {
      console.warn('Invalid format for name:', name);
      return name;
  }

  const parts = name.split('<br>');

  if (parts.length > 2) {
      parts.splice(2, 1);
  }

  return parts.join('<br>');
}

}
