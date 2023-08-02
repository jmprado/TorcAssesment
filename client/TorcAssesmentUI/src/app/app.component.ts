import { Component } from '@angular/core';
import { LoggedUser } from './models/loggedUser';
import { GlobalService } from './services/global-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TorcAssesmentUI';
  loggedUser!: LoggedUser;

  constructor(private globalService: GlobalService) {
    this.loggedUser = globalService.loggedUser.getValue();
  }
}
