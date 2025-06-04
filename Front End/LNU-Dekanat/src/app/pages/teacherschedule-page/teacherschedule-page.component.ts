import { Component } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { ScheduleService } from '../../core/services/schedule/schedule-service.service';
import { IScheduleItem } from '../../models/schedule/schedule.module';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SchudleLayoutComponent } from "../../layouts/schudle-layout/schudle-layout.component";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-teacherschedule-page',
  standalone: true,
  imports: [HeaderComponent, CommonModule, FormsModule, SchudleLayoutComponent],
  templateUrl: './teacherschedule-page.component.html',
  styleUrl: './teacherschedule-page.component.css'
})
export class TeacherschedulePageComponent {
  today: Date = new Date();

  schedule: {id: number; pairNumber: number; time: string; name: string; dayOfWeek: string;  }[] = [];

  selectedAction: string = ' ';

  scheduleMon: IScheduleItem[] = [];
  scheduleTue: IScheduleItem[] = [];
  scheduleWed: IScheduleItem[] = [];
  scheduleThue: IScheduleItem[] = [];
  scheduleFri: IScheduleItem[] = [];
  scheduleSat: IScheduleItem[] = [];

  userRole = localStorage.getItem("userRole");

  isShowed: boolean = false;

  constructor(private scheduleService: ScheduleService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.initTeacherSchedule();
  }

  defaultSchedule(): void {

    if(this.today.getDay() == 1)
    {
      this.schedule = this.scheduleMon;
      this.selectedAction = 'action1';
    }
    if(this.today.getDay() == 2)
    {
      this.schedule = this.scheduleTue;
      this.selectedAction = 'action2';
    }
    if(this.today.getDay() == 3)
    {
      this.schedule = this.scheduleWed;
      this.selectedAction = 'action3';
    }
    if(this.today.getDay() == 4)
    {
      this.schedule = this.scheduleThue;
      this.selectedAction = 'action4';
    }
    if(this.today.getDay() == 5)
    {
      this.schedule = this.scheduleFri;
      this.selectedAction = 'action5'
    }

  }

  performAction(): void {
    if (this.selectedAction === 'action1') 
    {

      this.schedule = this.scheduleMon;

    } else if (this.selectedAction === 'action2') 
    {

      this.schedule = this.scheduleTue;

    }
    else if(this.selectedAction === 'action3')
    {

      this.schedule = this.scheduleWed;

    } else if(this.selectedAction === 'action4') 
    {

      this.schedule = this.scheduleThue;

    }else if(this.selectedAction === 'action5')
    {

      this.schedule = this.scheduleFri;

    }else if(this.selectedAction === 'action6')
    {

      this.schedule = this.scheduleSat;

    }
  }

  private initTeacherSchedule() :void {
    const teacherId = this.route.snapshot.paramMap.get('id');
    if(teacherId){
    this.scheduleService.getTeacherSchedule(teacherId).subscribe((data: any[]) => {
      this.scheduleMon = data.filter(item => item.dayOfWeek === 'Понеділок');
      this.scheduleTue = data.filter(item => item.dayOfWeek === 'Вівторок');
      this.scheduleWed = data.filter(item => item.dayOfWeek === 'Середа');
      this.scheduleThue = data.filter(item => item.dayOfWeek === 'Четвер');
      this.scheduleFri = data.filter(item => item.dayOfWeek === 'П\'ятниця');
      this.scheduleSat = data.filter(item => item.dayOfWeek === 'Субота');

      this.defaultSchedule();
      });
  }
  }
}
