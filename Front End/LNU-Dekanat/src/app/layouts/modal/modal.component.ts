import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ScheduleService } from '../../core/services/schedule/schedule-service.service';
import { Group } from '../../models/departament/group.module';
import { UpdateSchedule } from '../../models/schedule/updateSchedule.module';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit, OnDestroy {


  isVisible = false;

  selectedGroup: number = 0;
  selectedType: string = 'Лекція';
  selectedSubgroup: string = 'Обидві';
  selectedDay: string = '';
  pairNumber: number = 1;
  pairTime: string = '';
  pairName: string = '';

  availableGroups: Group[] = [];

  parsedSubjectName: string[] = []

  updatedStudentSchedule: UpdateSchedule = {
    dayOfWeek: '',
    pairNumber: 0,
    time: '',
    name: ''
  }

  updatedTeacherSchedule: UpdateSchedule = {
    dayOfWeek: '',
    pairNumber: 0,
    time: '',
    name: ''
  }

  constructor(private scheduleService: ScheduleService) {}

  ngOnInit(): void {
    this.initGroups();
    this.initTeacherSubjects();
  }

  ngOnDestroy(): void {
    this.parsedSubjectName = [];
    this.availableGroups = [];
  }

  private initGroups(): void {
    this.scheduleService.getGroups().subscribe({
      next: (response) => {
        this.availableGroups = response;
        console.log(this.availableGroups);
      }
    });
  }

  private initTeacherSubjects(): void {

    const userId = localStorage.getItem("userId");
    if(userId) 
    {
      this.scheduleService.getTeacherSubjects(userId).subscribe({
        next: (response) =>
        {
          const names = response.map(item => item.name.split('<br>')[0].trim());
      
          this.parsedSubjectName = [...new Set(names)];  
        }
      })
    }
  }

  open() {
    this.ngOnInit();
    this.isVisible = true;
  }

  close() {
    this.ngOnDestroy();
    this.isVisible = false;
  }

  saveSchedule() {
    this.updatedStudentSchedule = {
      dayOfWeek: this.selectedDay,
      pairNumber: this.pairNumber,
      time: this.pairTime,
      name: `${this.pairName} <br> ${this.selectedType} <br> ${this.selectedSubgroup}`
    };

    this.updatedTeacherSchedule = {
      dayOfWeek: this.selectedDay,
      pairNumber: this.pairNumber,
      time: this.pairTime,
      name: `${this.pairName} <br> ${this.selectedType} <br> ${this.availableGroups[this.selectedGroup-1].groupName} <br> ${this.selectedSubgroup}`
    }

    this.scheduleService.updateStudentSchedule(this.selectedGroup, this.selectedDay, this.updatedStudentSchedule).subscribe({
      next: (response) => {
        
        this.selectedGroup = 0;
        this.selectedType = 'Лекція';
        this.selectedSubgroup = 'Обидві';
        this.selectedDay = '';
        this.pairNumber = 1;
        this.pairTime = '';
        this.pairName = '';
      },
      error: (err) => {
        console.error('Error updating schedule:', err);
      }
    });

    const userId = localStorage.getItem("userId");
    if(userId)
    this.scheduleService.updateTeacherSchedule(userId, this.selectedDay, this.updatedTeacherSchedule).subscribe({
      next: (response) =>
      {
        console.log(response)
      },
      error: (error) =>
      {
        console.log(error);
      }
    })
  }
}
