import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from '../../../models/login/login.module';
import { LoginService } from '../../../core/services/login/service.service';

@Component({
  selector: 'app-teacher-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './teacher-login.component.html',
  styleUrl: './teacher-login.component.css'
})
export class TeacherLoginComponent {
loginForm!: FormGroup;

model: LoginModel = { email: '', password: '' };

constructor(private fb: FormBuilder, private router: Router, private login: LoginService) {

  this.loginForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]], 
  });
}

onSubmit() {
  if(this.loginForm.valid)
    {

     this.model = this.loginForm.value;

      this.login.loginTeacher(this.model).subscribe({
        next: (response) =>
        {
          console.log(response);
          localStorage.setItem("userId", response.userId);
          localStorage.setItem("userRole", "admin");
          this.router.navigate([`Profile/${response.userId}`]);
        }
      })

    }
}

}
