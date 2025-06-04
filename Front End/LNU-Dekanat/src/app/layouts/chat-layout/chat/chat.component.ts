import { Component, OnInit, OnChanges, Input, ViewChild, ElementRef, ChangeDetectorRef, SimpleChanges, Output, EventEmitter, AfterViewInit, OnDestroy, ViewChildren, QueryList, AfterViewChecked } from '@angular/core';
import { UserService } from '../../../core/services/user/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../../../core/services/chat/chat.service';
import { ChatModel } from '../../../models/chat/chat.module';
import { SignalRService } from '../../../core/services/signalR/signal-r.service';
import { RouterLink } from '@angular/router';
import { GroupMembersModalComponent } from "../../group-members-modal/group-members-modal.component";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, GroupMembersModalComponent],
  styleUrls: ['./chat.component.css']
})

export class ChatComponent implements OnInit, OnChanges, AfterViewChecked, OnDestroy, AfterViewInit {
  @Input() selectedUser: any;
  @ViewChild('scrollBlock') private scrollBlock!: ElementRef;
  @ViewChildren('message') messageElements!: QueryList<ElementRef>;
  private observer!: IntersectionObserver;
  userCount = 0;
  userList = [];
  isModalOpen: boolean = false;
  newMessage: string = '';
  messages: ChatModel[] = [];
  @Output() triggerChatList = new EventEmitter<{ senderId: string, receiverId: string, chatType: string, text: string, time: Date }>();

  constructor(
    private userService: UserService,
    private cdr: ChangeDetectorRef,
    private chatService: ChatService,
    private signalR: SignalRService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedUser'] && this.selectedUser) {
      this.loadChatHistory();
      if(this.selectedUser.chatType === "Group")
      {
        this.getUsersCount();
      }
      this.cdr.detectChanges();
    }
  }

  ngOnInit(): void {
    if(this.userService.getSelectedUser())
    {
      this.selectedUser = this.userService.getSelectedUser();
    }

    this.signalR.startConnection(this.userService.getCurrentUserid());

    this.signalR.addMessageListener((message: ChatModel) => {
      if (
        this.selectedUser?.chatType === 'Private' &&
        (message.receiverId === this.getId() || message.senderId === this.getId())
      ) {
        this.messages.push(message);
        this.cdr.detectChanges();
        this.scrollToBottom();
      }

      const updateMessage = {
        senderId: message.senderId,
        receiverId: message.receiverId,
        text: message.text,
        time: message.sendAt,
        chatType: "Private"
      }
      this.triggerChatList.emit(updateMessage);
    });

    this.signalR.addGroupMessageListener((message: ChatModel) => {
      if (this.selectedUser.chatType === 'Group') {
        this.messages.push(message);
        this.cdr.detectChanges();
        this.scrollToBottom();
      }
      const updateMessage = {
        senderId: message.senderId,
        receiverId: message.receiverId,
        text: message.text,
        time: message.sendAt,
        chatType: "Group"
      }

      this.triggerChatList.emit(updateMessage);
    });

    this.signalR.onMessageRead((messageId: string) => {
      const message = this.messages.find(msg => msg.messageId === messageId);
      if (message) {
        message.isRead = true;
        this.cdr.detectChanges();
      }
    });

    if (this.selectedUser) {
      this.loadChatHistory();
      this.cdr.detectChanges();
    } else {
      console.warn('No user selected for chat!');
    }
  }
  
  ngAfterViewInit(): void {
    if (this.selectedUser) {
      this.loadChatHistory();
      this.cdr.detectChanges();
    }
  }

  ngAfterViewChecked() {
    if (this.messageElements) {
      this.messageElements.forEach(message => {
        if (!this.observer) {
          this.initializeObserver();
        }
        this.observer.observe(message.nativeElement);
      });
    }
  }

  ngOnDestroy() {
    if (this.observer) {
      this.observer.disconnect();
    }
  }

  loadChatHistory(): void {

   if(this.selectedUser.chatType === 'Private')
   {
    this.chatService.getChatHistory(this.userService.getCurrentUserid(), this.selectedUser.recieverId).subscribe({
      next: (response) => {
        this.messages = response;

        this.messages.forEach(message => {
          if(message.senderId !== this.userService.getCurrentUserid()){
            this.chatService.makeMessageAsRead(message.messageId).subscribe({
              next: (response) =>
              {
                console.log(response);
              },
              error: (error) =>
              {
                console.log(error);
              }
            })
            this.signalR.markMessageAsRead(message.messageId);
            this.cdr.detectChanges();
            this.scrollToBottom();
          }
        });
      },
      error: (error) => {
        console.error('Error loading chat history', error);
      }
    });
   }
   else{

    this.signalR.joinGroup(this.selectedUser.recieverId);

    this.chatService.getGroupMessageHistory(this.selectedUser.recieverId).subscribe({
      next: (response) => {
        this.messages = response;

        this.messages.forEach(message => {
          if(message.senderId !== this.userService.getCurrentUserid()){
            this.chatService.makeGroupMessageAsRead(message.messageId).subscribe({
              next: (response) =>
              {
                console.log(response);
              },
              error: (error) =>
              {
                console.log(error);
              }
            })
            this.signalR.markMessageAsRead(message.messageId);
            this.cdr.detectChanges();
            this.scrollToBottom();
          }
        });
      },
      error: (error) => {
        console.error('Error loading chat history', error);
      }
    });
   }

  }

  sendMessage(): void {
    if (this.newMessage.trim()) {
      const message: ChatModel = {
        messageId: " ",
        senderId: this.userService.getCurrentUserid(),
        senderName: localStorage.getItem("username") || undefined,
        receiverId: this.selectedUser.receiverId || this.selectedUser.id || this.selectedUser.recieverId,
        text: this.newMessage,
        sendAt: new Date(),
        isRead: false
      };

      this.selectedUser.text = message.text;
      this.selectedUser.time = message.sendAt;

      if(this.selectedUser.chatType === "Private")
      {
        this.chatService.sendMessage(message).subscribe({
          next: (response) => {
            console.log('Message sent successfully:', response);
            message.messageId = response.id;
            this.signalR.sendMessage(message);
            this.messages.push(message);
            this.cdr.detectChanges();
            this.scrollToBottom();
          },
          error: (error) => {
            console.error('Error sending message', error);
          }
        });
      }
      else
      {
        this.chatService.sentGroupMessage(message).subscribe({
          next: (response) => {
            console.log('Message sent successfully:', response);
            message.messageId = response.id;
            this.signalR.sendGroupMessage(message);
            this.cdr.detectChanges();
            this.scrollToBottom();
          },
          error: (error) => {
            console.error('Error sending message', error);
          }
        });
      }

      this.newMessage = ' '; 
    }
  }

  private scrollToBottom(): void {
    if (this.scrollBlock) {
      this.cdr.detectChanges();
      setTimeout(() => {
        this.scrollBlock.nativeElement.scrollTo({
          top: this.scrollBlock.nativeElement.scrollHeight,
          behavior: 'auto'
        });
      }, 0);
    }
  }

  private initializeObserver(): void {
    this.observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          const messageId = entry.target.getAttribute('data-message-id');
          const senderId = entry.target.getAttribute('data-sender-id');
          if (messageId && senderId !== this.userService.getCurrentUserid()) {
            this.signalR.markMessageAsRead(messageId);
            if(this.selectedUser.chatType === 'Private')
            {
              this.chatService.makeMessageAsRead(messageId).subscribe({
                next: (response) =>
                {
                  console.log(response);
                },
                error: (error) =>
                {
                  console.log(error);
                }
              })
            }
            else
            {
              this.chatService.makeGroupMessageAsRead(messageId).subscribe({
                next: (response) =>
                {
                  console.log(response);
                },
                error: (error) =>
                {
                  console.log(error);
                }
              })
            }
            this.cdr.detectChanges();
          }
        }
      });
    }, { threshold: 0.1 });
  }

  getId(): string {
    return this.userService.getCurrentUserid();
  }

  getUsersCount(): void {
    this.chatService.getGroupUsers(this.selectedUser.recieverId).subscribe({
      next: (response) => {
        this.userList = response;
        this.userCount = response.length;
      },
      error: (error) => {
        console.error('Помилка при отриманні кількості учасників:', error);
      }
    });
  }

  openModal() {
    this.isModalOpen = !this.isModalOpen;
  }

  closeModal() {
    this.getUsersCount();
    this.isModalOpen = false;
  }
}
