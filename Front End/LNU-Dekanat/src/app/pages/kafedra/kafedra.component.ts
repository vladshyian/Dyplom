import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { KafedraLayoutComponent } from "../../layouts/kafedra-layout/kafedra-layout.component";
import { Router } from '@angular/router';
import { DepartamentService } from '../../core/services/departament/departament.service';
import { DepartamentListModel } from '../../models/departament/departament.module';

@Component({
  selector: 'app-kafedra',
  standalone: true,
  imports: [HeaderComponent, KafedraLayoutComponent],
  templateUrl: './kafedra.component.html',
  styleUrl: './kafedra.component.css'
})
export class KafedraComponent implements OnInit {

  departaments: DepartamentListModel[] = [
    {
      id: 0,
      departamentName: "",
      teacherName: "",
      kafedtaPhoto: "",
      departamentPhone: "",
      departamentLink: ""
    }
  ]

  constructor(private router: Router, private departamentService: DepartamentService){}
  
  ngOnInit(): void {
    this.initDepartament();
  }

  onKafedraSelected(kafedraId: number): void {

    this.router.navigate([`KafedraInformation/${kafedraId}`]);
    
  }

  private initDepartament():void{
    this.departamentService.getDepartamentList().subscribe({
      next: (response) =>
      {
        this.departaments = response;
        console.log(this.departaments);
      }
    })
  }
}
