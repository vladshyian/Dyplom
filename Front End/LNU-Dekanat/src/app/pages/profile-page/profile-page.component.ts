import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { ProfileComponent } from "../../layouts/profile/profile.component";
import { UserService } from '../../core/services/user/user.service';
import { Student} from '../../models/user/student.module';
import { Teacher } from '../../models/user/teacher.module';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [HeaderComponent, ProfileComponent],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent implements OnInit {

  isTeacher: string | null = localStorage.getItem("userRole");
  currentUserId: string | null = localStorage.getItem("userId");
  profileUserId: string | null = null;

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
  
  constructor(private user: UserService, private cdr: ChangeDetectorRef, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.profileUserId = this.route.snapshot.paramMap.get('id');
    
    if (this.profileUserId === this.currentUserId) {

      if(this.getRole()) {

        this.initTeacher();
        console.log("we are here")
      
      } else {

        this.initStudent();
      
      }
    } else {

      this.checkProfileRole();
    
    }
    
    this.cdr.detectChanges();
  }

  getRole(): boolean {
    return this.isTeacher === 'admin';
  }

  private checkProfileRole(): void {
    if (this.isTeacher === 'student' || this.getRole()) {

      this.initTeacher();

    } else {

      this.initStudent();
    
    }
  }

  private initStudent(): void {
    if (this.profileUserId) {
      this.user.getStudentInfo(this.profileUserId).subscribe({
        next: (response) => {
            this.student = response;

            if(this.profileUserId === this.currentUserId) {
              this.user.updateUsername(this.student.coreStudent.name);            
            }
          }
      });
    }
  }

  private initTeacher(): void {
    if (this.profileUserId) {
      this.user.getTeacherInfo(this.profileUserId).subscribe({
        next: (response) => {
            this.teacher = response;

            console.log(this.teacher.coreInfo.name)

            if(this.profileUserId === this.currentUserId) {
              this.user.updateUsername(this.teacher.coreInfo.name);
            }
          }
      });
    }
  }
}
