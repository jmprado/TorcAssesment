import { Component, OnInit } from '@angular/core';
import { LoggedUser } from './models/loggedUser';
import { GlobalService } from './services/global-service';
import { Observable, ObservableNotification } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'TorcAssesmentUI';
  loggedUserObs!: any;
  loggedUser!: LoggedUser;
  isLogged: boolean = false;

  constructor(private globalService: GlobalService) {

  }

  ngOnInit(): void {
    this.loggedUserObs = this.globalService.loggedUser.subscribe(
      c => this.isLogged = c.isLogged,
      c => this.loggedUser = c
    );

    this.loggedUser = this.loggedUserObs.value;
    this.isLogged = this.loggedUser.isLogged;
    console.log(this.loggedUser)
  }
}
