import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-kafedra-layout',
  standalone: true,
  imports: [],
  templateUrl: './kafedra-layout.component.html',
  styleUrl: './kafedra-layout.component.css'
})
export class KafedraLayoutComponent {

  @Input() kafedra_photo:  string = '';
  @Input() kafedra_name: string = '';
  @Input() teacher_name: string = '';
  @Input() kafedra_phone: string = '';
  @Input() kafedraId: number = 0; 
  @Output() kafedraSelected = new EventEmitter<number>(); 

  onKafedraClick(): void {
    this.kafedraSelected.emit(this.kafedraId); 
  }
}
