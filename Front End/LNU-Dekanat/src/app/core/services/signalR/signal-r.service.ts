import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { ChatModel } from '../../../models/chat/chat.module';
import { ChatList } from '../../../models/chat/chatList.module';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  private chatSubject = new BehaviorSubject<ChatList[]>([]);
  public chatList$ = this.chatSubject.asObservable();


  startConnection(userId: string) {
      this.hubConnection = new signalR.HubConnectionBuilder()
<<<<<<< HEAD
          .withUrl(`http://localhost:7215/chatHub?userId=${encodeURIComponent(userId)}`)
=======
          .withUrl(`http://localhost:5208/chatHub?userId=${encodeURIComponent(userId)}`)
>>>>>>> 875b81d (Initial commit to main)
          .build();

      this.hubConnection
          .start()
          .then(() => console.log('Connection started'))
          .catch(err => console.error('Error while starting connection: ' + err));

      this.hubConnection.on('NewChat', (chat: ChatList) => {
      this.addOrUpdateChat(chat);
    });
  }

  addOrUpdateChat(chat: ChatList) {
    const chats = this.chatSubject.getValue();
    const existingChatIndex = chats.findIndex(c => c.recieverId === chat.recieverId);
    
    if (existingChatIndex >= 0) {
      chats[existingChatIndex] = chat; 
    } else {
      chats.push(chat);  
    }

    this.chatSubject.next([...chats]);
  }

  newChatUpdate(chat: ChatList) {
    this.hubConnection.invoke('NewChatUpdate', chat).catch(err => console.error(err));
  }

  sendMessage(message: ChatModel) {
      this.hubConnection.invoke('SendPrivateMessage', message)
          .catch(err => console.error('Error while sending message: ' + err));
  } 

  addMessageListener(callback: (message: ChatModel) => void) {
      this.hubConnection.on('ReceiveMessage', (message) => {
        callback(message);
      });
  }

  onMessageRead(callback: (messageId: string) => void) {
    this.hubConnection.on('MarkAsRead', callback);
  }

  markMessageAsRead(messageId: string) {
    this.hubConnection.invoke('MarkMessage', messageId)
      .catch(err => console.error('Error while marking message as read: ', err));
  }

  joinGroup(groupId: string) {
    this.hubConnection.invoke("JoinGroup", groupId)
      .then(() => {
        console.log(`User joined group ${groupId}`);
      })
      .catch(err => console.error("Error joining group:", err));
  }

  addGroupMessageListener(callback: (message: ChatModel) => void) {
    this.hubConnection.on('ReceiveGroupMessage', (message) => {
      callback(message);
    });
  }

  sendGroupMessage(message: ChatModel) {
    this.hubConnection.invoke("SendGroupMessage", message)
      .then(() => {
        console.log('Group message sent via SignalR');
      })
      .catch(err => console.error("Error sending group message via SignalR:", err));
  }

  newGroupChatUpdate(chat: ChatList) {
    this.hubConnection.invoke('NewChatUpdate', chat).catch(err => console.error(err));
  }
}
