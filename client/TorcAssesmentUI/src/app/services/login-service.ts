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
    'Content-type': 'application/json',
    'Accept': 'application/json'
  }

  loginUser(loginUser: LoginUser) {
    const url = `${this.baseUrlApi}/security`;
    const body = JSON.stringify({ 'username': loginUser.username, 'password': loginUser.password });
    return axios.post<LoggedUser>(url, body, { headers: this.headerDict });
  }
}
