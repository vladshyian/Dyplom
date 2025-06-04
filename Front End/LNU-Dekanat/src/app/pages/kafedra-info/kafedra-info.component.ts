import { Component, Input } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { KafedraMenuComponent } from "../../layouts/kafedra-menu/kafedra-menu.component";
import { AboutKafedraComponent } from "../../layouts/kafedra-information/about-kafedra/about-kafedra.component";
import { KafedrasMenuComponent } from "../../layouts/kafedras-menu/kafedras-menu.component";
import { WorkersOnKafedraComponent } from "../../layouts/kafedra-information/workers-on-kafedra/workers-on-kafedra.component";
import { GoalsComponent } from "../../layouts/kafedra-information/goals/goals.component";
import { TeacherScheduleComponent } from "../../layouts/kafedra-information/teacher-schedule/teacher-schedule.component";
import { ActivatedRoute } from '@angular/router';
import { DepartamentService } from '../../core/services/departament/departament.service';
import { DepartamentAboutModel } from '../../models/departament/departamentAbout.module';
import { DepartamentListModel } from '../../models/departament/departament.module';
import { DepartamentTeachets } from '../../models/departament/departamentTeachers.module';

@Component({
  selector: 'app-kafedra-info',
  standalone: true,
  imports: [HeaderComponent, KafedraMenuComponent, AboutKafedraComponent, KafedrasMenuComponent, WorkersOnKafedraComponent, GoalsComponent, TeacherScheduleComponent],
  templateUrl: './kafedra-info.component.html',
  styleUrl: './kafedra-info.component.css'
})
export class KafedraInfoComponent {

  departamentData: DepartamentAboutModel = {
    departamentAbout: "",
    departamentMission: "",
    departamentVizia: "",
    departamnetStrategy: "",
  };

  departament: DepartamentListModel = 
    {
      id: 0,
      departamentName: "",
      teacherName: "",
      kafedtaPhoto: "",
      departamentPhone: "",
      departamentLink: ""
  };

  teachers: DepartamentTeachets[] = []

  selectedMenu: string = 'about'; 
  
  constructor(private route: ActivatedRoute, private departamentService: DepartamentService) {}

  ngOnInit(): void {
    const kafedraId = Number(this.route.snapshot.paramMap.get('id'));

    this.initKafedraAbout(kafedraId);
    this.initKafedra(kafedraId);
    this.initTeachers(kafedraId);

  }

  onMenuSelected(menu: string): void {
    this.selectedMenu = menu; 
  }

  private initKafedraAbout(Id: number)
  {
    this.departamentService.getDepartamentAbout(Id).subscribe({
      next: (response) =>
      {
        this.departamentData = response;

        console.log(response);
      }
    })
  }

  private initKafedra(Id: number) {
    this.departamentService.getDepartamentById(Id).subscribe({
      next: (response) => 
      {
        this.departament = response;
      }
    })
  }

  private initTeachers(Id:number)
  {
    this.departamentService.getDepartamentTeachers(Id).subscribe({
      next: (response) =>
      {
        this.teachers = response;
      }
    })
  }
}
