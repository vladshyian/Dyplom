import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../core/services/user/user.service';
import { ChatService } from '../../core/services/chat/chat.service';

@Component({
  selector: 'app-modal-user-list',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './modal-user-list.component.html',
  styleUrl: './modal-user-list.component.css'
})
export class ModalUserListComponent {
  @Input() isOpen: boolean = false; 
  @Input() users: any[] = []; 
  @Output() close = new EventEmitter<void>();
  @Output() userSelected = new EventEmitter<any>(); 
  @Output() groupSelected = new EventEmitter<any>();
  groupName: string = '';
  isUserSelect = true;
  userRole = localStorage.getItem("userRole");

  constructor(private userService: UserService, private chatService: ChatService) {}

  closeModal(): void {
    this.close.emit();
  }

  onGroupSelect() {
    this.isUserSelect = !this.isUserSelect;
  }

  selectUser(user: any): void {
    this.userSelected.emit(user);
    this.closeModal();
  }

  createGroup(): void {
    const selectedUsers = this.users.filter(user => user.id);
  
    if (!this.groupName) {
      alert('Введіть назву групи');
      return;
    }
  
    if (selectedUsers.length === 0) {
      alert('Виберіть хоча б одного користувача');
      return;
    }

    const currentUserId = this.userService.getCurrentUserid(); 
    const currentUserName = localStorage.getItem("username");
  
    const groupData = {
      Id: " ",
      groupName: this.groupName,
      adminId: this.userService.getCurrentUserid(),
      userIds: [currentUserId, ...selectedUsers.map(user => user.id)],
      userName: [currentUserName, ...selectedUsers.map(user => user.recieverName)],
      chatType: "Group",
    };
  
    this.chatService.createGroup(groupData).subscribe({
      next: (response) => {
        groupData.Id = response.id;
        this.userSelected.emit(groupData);
      },
      error: (error) => 
      {
        console.log(error);
      }
    })
  
  }
}
