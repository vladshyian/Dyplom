import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-kafedra-menu',
  standalone: true,
  imports: [],
  templateUrl: './kafedra-menu.component.html',
  styleUrl: './kafedra-menu.component.css'
})
export class KafedraMenuComponent {

@Output() selectedMenu = new EventEmitter<string>();

selectMenuItem(menu: string) {
  this.selectedMenu.emit(menu);
}

}
