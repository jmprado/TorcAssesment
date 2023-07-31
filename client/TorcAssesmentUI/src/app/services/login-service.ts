import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { LoggedUser } from '../models/loggedUser';
import { LoginUser } from '../models/loginUser';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(public http: HttpClient) { }
  baseUrlApi = "https://localhost:7210"

  loginUser(loginUser: LoginUser){
    var url = `${this.baseUrlApi}/security}`;
    var body = JSON.stringify({'username': loginUser.username, 'password': loginUser.password});
    return new Promise<LoggedUser>(resolve => {
      this.http.post<LoggedUser>(url, body).subscribe(data => {
        resolve(data);
      },
      err => {
        console.log(err);
      })
    })
  }
}
