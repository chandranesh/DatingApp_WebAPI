import { Component, Input, signal } from '@angular/core';
import { Register } from '../account/register/register';
import { User } from '../../types/User';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  //@Input({required:true}) membersFromApp: User[] = [];
  protected registerMode = signal(false);
$event: any;
  showRegister(value: boolean) {
    this.registerMode.set(value);
  }
}
