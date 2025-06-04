import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { UserService } from '../../core/services/user/user.service';
import { ChatService } from '../../core/services/chat/chat.service';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-group-members-modal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './group-members-modal.component.html',
  styleUrl: './group-members-modal.component.css'
})
export class GroupMembersModalComponent {
  
  @Input() isOpen: boolean = false; 
  @Input() users: any[] = []; 
  @Input() groupId: string = "";;
  @Output() close = new EventEmitter<void>();
  isAddUser: boolean = false;
  userList: any[] = [];
  currentGroupUsers: any[] = [];

  constructor(private userService: UserService, private chatService: ChatService, private cdr: ChangeDetectorRef) {}

  closeModal(): void {
    this.close.emit();
  }

  getId() : string
  {
    return this.userService.getCurrentUserid();
  }

  deleteSelectedUsers(): void {
    const selectedUserIds = this.users
      .filter(user => user.selected)
      .map(user => user.id);
  
    if (selectedUserIds.length > 0) {
      this.chatService.deleteUsersFromGroup(selectedUserIds, this.groupId)
        .subscribe({
          next: (response) => {
              this.users = this.users.filter(user => !selectedUserIds.includes(user.id));
              console.log(this.users);
          },
          error: (err) => console.error('Помилка API:', err)
        });
    } else {
      console.log('Жоден користувач не вибраний для видалення');
    }
  }

  addMembersToDB() {
    const selectedUsers = this.userList
      .filter(user => user.selected) 
      .map(user => ({ id: user.id, recieverName: user.recieverName, userPhoto: " " }));  

    this.chatService.addUsersToGroup(selectedUsers, this.groupId).subscribe({
      next: (response) => {
        const selectedUserIds = selectedUsers.map(user => user.id);
        this.userList = this.userList.filter(user => !selectedUserIds.includes(user.id));
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
  
  addMembers() {
    forkJoin({
      allUsers: this.userService.getAllUsers(this.getId()),
      groupUsers: this.chatService.getGroupUsers(this.groupId),
    }).subscribe({
      next: ({ allUsers, groupUsers }) => {
        this.currentGroupUsers = groupUsers;
  
        this.userList = allUsers.filter((user: { id: any; }) => 
          !this.currentGroupUsers.some(groupUser => groupUser.id === user.id)
        );
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.log('Помилка при завантаженні даних:', error);
      },
    });
  
    this.isAddUser = !this.isAddUser;
  }
}
