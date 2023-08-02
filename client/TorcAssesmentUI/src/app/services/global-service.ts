import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoggedUser } from '../models/loggedUser';

@Injectable({
  providedIn: 'root'
})


export class GlobalService {
  public loggedUser = new BehaviorSubject<LoggedUser>({
    id: 0,
    username: "",
    role: "",
    token: "",
    isLogged: false
  });

  public baseUrlApi: string = "https://localhost:7210/api";
}