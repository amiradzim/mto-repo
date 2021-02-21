import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component ({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'Material Take-Off';
  users: any;

  constructor(private accountService: AccountService) {}

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.setCurrentUser();
  }

  // tslint:disable-next-line:typedef
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

}

