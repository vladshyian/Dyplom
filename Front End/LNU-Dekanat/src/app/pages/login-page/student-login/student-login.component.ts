import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../../core/services/login/service.service';
import { LoginModel } from '../../../models/login/login.module';

@Component({
  selector: 'app-student-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './student-login.component.html',
  styleUrl: './student-login.component.css'
})
export class StudentLoginComponent {

  loginForm!: FormGroup
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

      this.login.loginStudent(this.model).subscribe({
        next: (response) =>
        {
          console.log(response);
          localStorage.setItem("userId", response.userId);
          localStorage.setItem("userRole", "student");
          this.router.navigate([`Profile/${response.userId}`]);        
        }
      })

    }
  }
}
