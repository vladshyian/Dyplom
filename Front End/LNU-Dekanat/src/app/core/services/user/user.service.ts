import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Student } from '../../../models/user/student.module';
import { BehaviorSubject, Observable } from 'rxjs';
import { Teacher } from '../../../models/user/teacher.module';

@Injectable({
  providedIn: 'root'
})
export class UserService {

<<<<<<< HEAD
  path = "http://localhost:7215"
=======
  path = "http://localhost:5208"
>>>>>>> 875b81d (Initial commit to main)

  private usernameSubject = new BehaviorSubject<string | null>(localStorage.getItem('username'));
  private selectedUser: any;
  username$ = this.usernameSubject.asObservable();

  constructor(private http: HttpClient) { }

  updateUsername(username: string): void {
    localStorage.setItem('username', username);
    this.usernameSubject.next(username);
  }

  getStudentInfo(id: string): Observable<Student> {
    return this.http.get<Student>(`${this.path}/getStudent/${id}`);
  }
  
  getTeacherInfo(id: string): Observable<Teacher> {
    return this.http.get<Teacher>(`${this.path}/getTeacher/${id}`);
  }

  getAllUsers(id: string): Observable<any> {
    return this.http.get<any>(`${this.path}/getAllUsers/${id}`);
  }

  getStudentsByGroup(name: string):Observable<any> {
    return this.http.get<any>(`${this.path}/getStudentsByGroup/${name}`);
  }

  getCurrentUserid(): string {
    const id = localStorage.getItem("userId");
    if(id)
    {
      return id;
    }
    return "";
  }

  getUserName(): string {
    const name = localStorage.getItem("username");
    if(name)
    {
      return name;
    }
    return "";
  }

  setSelectedUser(user: any) {
    this.selectedUser = user;
  }

  getSelectedUser(): any {
    return this.selectedUser;
  }
}
