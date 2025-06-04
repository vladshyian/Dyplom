import {Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { LoginService } from '../../core/services/login/service.service';
import { UserService } from '../../core/services/user/user.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {

  username: string | null = '';
  userId = localStorage.getItem("userId");

  constructor(private auth: LoginService, private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.userService.username$.subscribe((username) => {
      this.username = username; 
    });  }

    goToProfile(): void {
      this.router.navigate([`/Profile`, this.userId]).then(() => {
        window.location.reload();
      });
    }

  logOut(): void {
    this.auth.logOut();
    this.router.navigate([""]);
  }
}
