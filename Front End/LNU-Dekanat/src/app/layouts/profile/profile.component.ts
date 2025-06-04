import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { HiddenContainerComponent } from "../hidden-container/hidden-container.component";
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ChatListComponent } from "../chat-layout/chat-list/chat-list.component";

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [HiddenContainerComponent, RouterLink, ChatListComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  isTeacher: string | null = localStorage.getItem("userRole");
  userRole: boolean = false;
  currentUserId: string | null = localStorage.getItem("userId");
  profileUserId: string | null = "";
  userId = "";

  @Input() kafedra: string = 'Кафедра';
  @Input() posada: string = 'Посада';
  @Input() stupin: string = 'Ступінь';
  @Input() zvannya: string = 'Звання';
  @Input() username: string  = 'Name';
  @Input() phone: string = 'Телефон';
  @Input() email: string = 'Пошта';
  @Input() group: string = 'Група';
  @Input() formOfStudy: string = 'Форма навчання';
  @Input() formOfPay: string = 'Форма оплати';
  @Input() termin: string = 'Термін навчання';
  @Input() endOfStudy: string = "Дата закінчення навчання";
  @Input() areasText: string = ' ';
  @Input() infoText: string = ' ';
  @Input() bioText: string = ' ';
  @Input() projectsText: string = ' ';
  @Input() photo: string = '';
  currentRoute: string;
  targetUserId: any;

  @ViewChild(ChatListComponent) chatListComponent!: ChatListComponent;

  constructor(private router: Router, private route: ActivatedRoute) {

    this.currentRoute = this.router.url;
  }

  ngOnInit(): void {
    this.profileUserId = this.route.snapshot.paramMap.get('id');
    
    if(this.profileUserId)
    {
      this.userId = this.profileUserId;
    }
    
    if (this.profileUserId === this.currentUserId) {
      this.userRole=false;
    }
    else{
      this.userRole = true;
    }

  }

  openChat(): void {
    console.log("cat")
    if (this.chatListComponent && this.profileUserId) {
      console.log("ok");
      this.chatListComponent.openChatById(this.profileUserId);
    }
  }

  isProfilePage(): boolean {
    return this.currentUserId === this.profileUserId;
  }

}
