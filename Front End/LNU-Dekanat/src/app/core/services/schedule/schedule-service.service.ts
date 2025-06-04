import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IScheduleItem } from '../../../models/schedule/schedule.module';
import { Group } from '../../../models/departament/group.module';
import { UpdateSchedule } from '../../../models/schedule/updateSchedule.module';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

<<<<<<< HEAD
  private path = "http://localhost:7215"; 
=======
  private path = "http://localhost:5208"; 
>>>>>>> 875b81d (Initial commit to main)

  constructor(private http: HttpClient) { }

  public getSchedule(groupName: string): Observable<IScheduleItem[]> {
    return this.http.get<IScheduleItem[]>(`${this.path}/getStudentSchedule/${groupName}`);
  }

  public getTeacherSchedule(teacherId: string): Observable<IScheduleItem[]> {
    return this.http.get<IScheduleItem[]>(`${this.path}/getTeacherSchedule/${teacherId}`);
  }

  public getGroups(): Observable<Group[]> {
    return this.http.get<Group[]>(`${this.path}/getGroups`);
  }

  public updateStudentSchedule(id: number, dayOfWeek: string, schedule: UpdateSchedule): Observable<any> {
    return this.http.put(`${this.path}/Schedule/updateSchedule/${id}/${dayOfWeek}`, schedule);
  }

  public updateTeacherSchedule(id: string, dayOfWeek: string, schedule: UpdateSchedule): Observable<any> {
    return this.http.put(`${this.path}/Schedule/updateTeacherSchedule/${id}/${dayOfWeek}`, schedule);
  }

  public getTeacherSubjects(id: string): Observable<IScheduleItem[]>
  {
    return this.http.get<IScheduleItem[]>(`${this.path}/Schedule/getTeacherSubjects/${id}`)
  }

  public deleteSchedule(schedule: IScheduleItem): Observable<any>
  {
    return this.http.post<any>(`${this.path}/Schedule/deleteSchedule`, schedule )
  }
}
