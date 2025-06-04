import { ChangeDetectorRef, Component, EventEmitter, Input, NgZone, OnChanges, OnInit, Output } from '@angular/core';
import { ChatWithComponent } from "../chat-with/chat-with.component";
import { ModalUserListComponent } from "../../modal-user-list/modal-user-list.component";
import { UserService } from '../../../core/services/user/user.service';
import { ScheduleService } from '../../../core/services/schedule/schedule-service.service';
import { ChatService } from '../../../core/services/chat/chat.service';
import { SignalRService } from '../../../core/services/signalR/signal-r.service';
import { ChatList } from '../../../models/chat/chatList.module';
import { Router } from '@angular/router';


@Component({
  selector: 'app-chat-list',
  standalone: true,
  imports: [ChatWithComponent, ModalUserListComponent],
  templateUrl: './chat-list.component.html',
  styleUrl: './chat-list.component.css'
})
export class ChatListComponent implements OnInit, OnChanges{
  isModalOpen: boolean = false; 
  availableUsers: any[] = [ ];
  chatUsers: any[] = []; 
  @Input() selectedUserId: string | null = null;
  @Output() userSelected = new EventEmitter<any>();  

  constructor(private userService: UserService, private chatService: ChatService,
    private signalR: SignalRService, private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnChanges(): void {
    if (this.selectedUserId) {
      this.openChatById(this.selectedUserId);
    }
  }

  ngOnInit(): void {
    this.signalR.startConnection(this.userService.getCurrentUserid());

    this.signalR.chatList$.subscribe((chats: ChatList[]) => {
      const newChats = chats
        .filter(chat => chat.recieverId !== this.userService.getCurrentUserid())
        .filter(chat => !this.chatUsers.some(existingChat => existingChat.recieverId !== chat.recieverId));
    
      this.chatUsers = [...this.chatUsers, ...newChats];
    });

    this.chatService.getUserChats(this.userService.getCurrentUserid()).subscribe({
      next: (response) =>
      {
        this.chatUsers = response;
      }
    })

    if(this.userService.getCurrentUserid()){
      this.userService.getAllUsers(this.userService.getCurrentUserid()).subscribe({
        next: (response) =>
        {
          this.availableUsers = response;
        }
      })
    }
  }

  public updateMessages(senderId: string, receiverId: string, chatType: string, text: string, timestamp: Date): void {

    if(chatType === 'Private') {
      const user = this.chatUsers.find(u => u.recieverId === senderId);
      if (user) {
        user.text = text;
        user.time = timestamp;
        this.cdr.detectChanges();
      }
    }
    else{
      
    }
  }

  openModal(): void {
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
  }

  handleUserSelected(user: any): void {
    const existingChat = this.chatUsers.find(u => u.recieverId === user.id || u.senderId === user.id);
    
    if (!existingChat) {
      const newChat: ChatList = {
        senderId: this.userService.getCurrentUserid(),
        recieverId: user.id || user.Id,
        recieverName: user.recieverName || user.groupName,
        senderName: this.userService.getUserName(),
        text: " ",
        time: new Date().toISOString(),
        userPhoto: user.userPhoto,
        chatType: user.chatType || "Private",
      };
      
      this.chatUsers.push(newChat);
      this.cdr.detectChanges();
  
      const reverseChat: ChatList = {
        senderId: user.id,
        recieverId: this.userService.getCurrentUserid(),
        recieverName: this.userService.getUserName() ||  user.groupName,
        senderName: user.recieverName,
        text: " ",
        time: new Date().toISOString(),
        chatType: user.chatType || "Private",
      };
  
      this.signalR.newChatUpdate(reverseChat);
    }
    
    this.closeModal();
  }
  
  onSelectUser(user: any): void {
    console.log(user);
    this.userSelected.emit(user);
    this.userService.setSelectedUser(user);
  }

  openChatById(userId: string): void {
    const existingChat = this.chatUsers.find(chat => chat.recieverId === userId || chat.senderId === userId);
  
    if (existingChat) {
      this.onSelectUser(existingChat);
      this.router.navigate(["Chats"]);
    } else {
      const user = this.availableUsers.find(u => u.id === userId);
      if (user) {
        this.handleUserSelected(user); 
      } else {
        console.warn('Користувача з таким ID не знайдено серед доступних.');
      }
    }
  }
  
}
