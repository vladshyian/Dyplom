import { Component } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { ScorecardLayoutComponent } from "../../layouts/scorecard-layout/scorecard-layout.component";

@Component({
  selector: 'app-scorecard',
  standalone: true,
  imports: [HeaderComponent, ScorecardLayoutComponent],
  templateUrl: './scorecard.component.html',
  styleUrl: './scorecard.component.css'
})
export class ScorecardComponent {

  courses = [
    {id: 1, name: "Системи нечіткої логіки", labs: 12},
    {id: 2, name: "Теорії прийняття рішень", labs: 10},
    {id: 3, name: "Цифрове опрацювання зображень", labs: 10},
    {id: 4, name: "Інновації і підприємництво в ІТ", labs: 5},
    {id: 5, name: "Нейронні мережі", labs: 10},
    {id: 6, name: "Управління ІТ-проектами", labs: 10}
  ]

}
