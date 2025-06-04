import { Component, EventEmitter, input, Input } from '@angular/core';
import { ChatComponent } from "../chat/chat.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat-with',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './chat-with.component.html',
  styleUrl: './chat-with.component.css'
})
export class ChatWithComponent {

  @Input() user: any;
  userRole = localStorage.getItem("userRole");
  selectedChat: any = null; 

  selectChat(chat: any): void {
    this.selectedChat = chat;
  } 
}
