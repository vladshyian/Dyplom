import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { UserService } from '../../core/services/user/user.service';
import { Student } from '../../models/user/student.module';
import { Teacher } from '../../models/user/teacher.module';

@Component({
  selector: 'app-setting-page',
  standalone: true,
  imports: [HeaderComponent],
  templateUrl: './setting-page.component.html',
  styleUrl: './setting-page.component.css'
})
export class SettingPageComponent implements OnInit {

  selectedFile: File | null = null;

  student: Student = {
    coreStudent: {
      id: "",
      name: "",
      email: "",
      password: null,
      phone: ""
    },
    academicalStudent: {
      stupin: "",
      group: "",
      formOfStudy: "",
      formOfPay: "",
      terminOfStudy: "",
      endOfStudy: ""
    }
  };

  teacher: Teacher = {
    coreInfo: {
      id: "",
      name: "",
      email: "",
      phone: "",
      departament: null,
      title: null,
      photoPath: ""
    },
    academicDetails: {
      stupin: "",
      rank: "",
      departament: "",
      title: ""
    },
    additionalInfo: {
      research: "",
      subjects: "",
      biography: "",
      projects: ""
    }
  };

  surname: string = '';
  name: string = '';

  userRole = localStorage.getItem("userRole");

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    const userId = localStorage.getItem("userId");

    if(userId && this.userRole === 'student')
    {
      this.userService.getStudentInfo(userId).subscribe({
        next: (response) =>
        {
          this.student = response;
          [this.surname, this.name] = this.student.coreStudent.name.split(' ');
        }
      })
    }
    else if(userId && this.userRole === 'admin')
    {
      this.userService.getTeacherInfo(userId).subscribe({
        next: (response) =>
        {
          this.teacher = response;
          [this.surname, this.name] = this.teacher.coreInfo.name.split(' ');
        }
      })
    }
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
    }
  }

}
