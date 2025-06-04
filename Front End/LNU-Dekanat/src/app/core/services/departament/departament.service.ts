import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DepartamentListModel } from '../../../models/departament/departament.module';
import { DepartamentAboutModel } from '../../../models/departament/departamentAbout.module';
import { DepartamentTeachets } from '../../../models/departament/departamentTeachers.module';

@Injectable({
  providedIn: 'root'
})
export class DepartamentService {

<<<<<<< HEAD
  private path = "http://localhost:7215/Departament"; 
=======
  private path = "http://localhost:5208/Departament"; 
>>>>>>> 875b81d (Initial commit to main)

  constructor(private http: HttpClient ) { }

  public getDepartamentList(): Observable<DepartamentListModel[]> {
    return this.http.get<DepartamentListModel[]>(`${this.path}/getDepartamentList`);
  }

  public getDepartamentAbout(Id: number): Observable<DepartamentAboutModel>{
    return this.http.get<DepartamentAboutModel>(`${this.path}/getDepartamentAbout/${Id}`)
  }

  public getDepartamentById(Id: number): Observable<DepartamentListModel> {
    return this.http.get<DepartamentListModel>(`${this.path}/getDepartamentById/${Id}`)
  }

  public getDepartamentTeachers(Id: number):Observable<DepartamentTeachets[]>{
    return this.http.get<DepartamentTeachets[]>(`${this.path}/getTeachersByDepartament/${Id}`)
  }
  
}
