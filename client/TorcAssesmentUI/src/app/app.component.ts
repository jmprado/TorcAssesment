import { Component } from '@angular/core';
import { LoggedUser } from './models/loggedUser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TorcAssesmentUI';
  loggedUser!: LoggedUser;  
}
