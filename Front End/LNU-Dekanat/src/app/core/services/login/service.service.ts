import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginModel } from '../../../models/login/login.module';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

<<<<<<< HEAD
  private path = "http://localhost:7215/Login";
=======
  private path = "http://localhost:5208/Login";
>>>>>>> 875b81d (Initial commit to main)

  constructor(private http: HttpClient) { }

  loginStudent(loginForm: LoginModel): Observable<any> {

    return this.http.post<any>(`${this.path}/LoginStudent`, loginForm);

  }

  loginTeacher(loginForm: LoginModel): Observable<any> {

    return this.http.post<any>(`${this.path}/LoginTeacher`, loginForm);

  }

  logOut(): void {
    localStorage.clear();
  }
  
}
