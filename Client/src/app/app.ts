import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Home } from '../features/home/home';
import { User } from '../types/User';
//import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
 // imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Nav, Home]
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  protected readonly title = 'Dating App';
  private http = inject(HttpClient);
  protected members = signal<User[]>([]);

  async ngOnInit() {
    this.members.set(await this.getMembers());
    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if (!userString)
      return;// user string not found
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
  
  async getMembers() {
    try{
    return lastValueFrom(this.http.get<User[]>('https://localhost:5078/api/members'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
