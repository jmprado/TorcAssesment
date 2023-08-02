import { Component, OnInit } from '@angular/core';
import { LoggedUser } from '../../models/loggedUser';
import { GlobalService } from '../../services/global-service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  loggedUser!: LoggedUser;

  constructor(private globalService: GlobalService) { }

  ngOnInit(): void {
    this.loggedUser = this.globalService.loggedUser.getValue();
  }

}
