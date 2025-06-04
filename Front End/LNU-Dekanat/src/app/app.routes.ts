import { Routes } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { TeacherLoginComponent } from './pages/login-page/teacher-login/teacher-login.component';
import { StudentLoginComponent } from './pages/login-page/student-login/student-login.component';
<<<<<<< HEAD
import { HeaderComponent } from './layouts/header/header.component';
=======
>>>>>>> 875b81d (Initial commit to main)
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { HiddenContainerComponent } from './layouts/hidden-container/hidden-container.component';
import { ScorecardComponent } from './pages/scorecard/scorecard.component';
import { ScheduleComponent } from './pages/schedule/schedule.component';
import { KafedraComponent } from './pages/kafedra/kafedra.component';
import { KafedraInfoComponent } from './pages/kafedra-info/kafedra-info.component';
import { KafedraMenuComponent } from './layouts/kafedra-menu/kafedra-menu.component';
import { AboutKafedraComponent } from './layouts/kafedra-information/about-kafedra/about-kafedra.component';
import { SettingPageComponent } from './pages/setting-page/setting-page.component';
import { ChatPageComponent } from './pages/chat-page/chat-page.component';
import { scheduled } from 'rxjs';
import { SchudleLayoutComponent } from './layouts/schudle-layout/schudle-layout.component';
import { TeacherschedulePageComponent } from './pages/teacherschedule-page/teacherschedule-page.component';
import { NavantaComponent } from './pages/navanta/navanta.component';
<<<<<<< HEAD
=======
import { HeaderComponent } from './layouts/header/header.component';

>>>>>>> 875b81d (Initial commit to main)

export const routes: Routes = [

    {
<<<<<<< HEAD
        path: "",
        component: LoginPageComponent
    },
    {
        path: "LoginTeacher",
        component: TeacherLoginComponent
    },
    {
        path: "LoginStudent",
        component: StudentLoginComponent
    },
    {
        path: "Profile/:id",
        component: ProfilePageComponent
    },
    {
        path: "Scorecard",
        component: ScorecardComponent
    },
    {
        path: "Schedule",
        component: ScheduleComponent
    },
    {
        path: "Kafedra",
        component: KafedraComponent
    },
    {
        path: "KafedraInformation/:id",
        component: KafedraInfoComponent,
    },
    {
        path: "Settings",
        component: SettingPageComponent
    },
    {
        path: "Chats",
        component: ChatPageComponent
    },
    {
        path: "TeacherSchedule/:id",
        component: TeacherschedulePageComponent
    },
    {
        path: "Navantazhennya",
        component: NavantaComponent
    }
=======
    path: "",
    component: LoginPageComponent
  },
  {
    path: "LoginTeacher",
    component: TeacherLoginComponent
  },
  {
    path: "LoginStudent",
    component: StudentLoginComponent
  },
  {
    path: "Profile/:id",
    component: ProfilePageComponent
  },
  {
    path: "Scorecard",
    component: ScorecardComponent
  },
  {
    path: "Schedule",
    component: ScheduleComponent
  },
  {
    path: "Kafedra",
    component: KafedraComponent
  },
  {
    path: "KafedraInformation/:id",
    component: KafedraInfoComponent
  },
  {
    path: "Settings",
    component: SettingPageComponent
  },
  {
    path: "Chats",
    component: ChatPageComponent
  },
  {
    path: "TeacherSchedule/:id",
    component: TeacherschedulePageComponent
  },
  {
    path: "Navantazhennya",
    component: NavantaComponent
  } 
>>>>>>> 875b81d (Initial commit to main)
];
