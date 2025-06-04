import { Component, ViewChild } from '@angular/core';
import { HeaderComponent } from "../../layouts/header/header.component";
import { ChatListComponent } from "../../layouts/chat-layout/chat-list/chat-list.component";
import { ChatComponent } from "../../layouts/chat-layout/chat/chat.component";

@Component({
  selector: 'app-chat-page',
  standalone: true,
  imports: [HeaderComponent, ChatListComponent, ChatComponent],
  templateUrl: './chat-page.component.html',
  styleUrl: './chat-page.component.css'
})
export class ChatPageComponent {

  selectedUser: any = null; 
  @ViewChild(ChatListComponent) chatList!: ChatListComponent;

  updateMessage(senderId: string, receiverId: string, chatType: string, text: string, time: Date) {
    this.chatList.updateMessages(senderId, receiverId, chatType, text, time); 
  }

  selectUser(user: any): void {
    this.selectedUser = user;
  }
}
