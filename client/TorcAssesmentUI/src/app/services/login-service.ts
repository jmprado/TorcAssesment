import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { LoggedUser } from '../models/loggedUser';
import { LoginUser } from '../models/loginUser';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(public http: HttpClient) {

   }
  baseUrlApi = "https://localhost:7210/api";
  headerDict = {
    'Content-type' : 'application/json', 
    'Accept' : 'application/json'
  }
  
  requestOptions = {                                                                                                                                                                                 
    headers: new HttpHeaders(this.headerDict), 
  };

  loginUser(loginUser: LoginUser){
      var url = `${this.baseUrlApi}/security`;
      var body = JSON.stringify({'username': loginUser.username, 'password': loginUser.password});
      return axios.post<LoggedUser>(url, body, { headers: this.headerDict });

    /*return new Promise<LoggedUser>(resolve => {
      (data => {
        resolve(data);
      },
      err => {
        console.log(err);
      })
    })*/    
  }
}
