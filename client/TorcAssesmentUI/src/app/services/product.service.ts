import { Injectable } from '@angular/core';
import { GlobalService } from './global-service';
import { HttpHeaders } from '@angular/common/http';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private globalService: GlobalService) { }

  baseUrlApi = "https://localhost:7210/api";

  headerDict = {
    'Content-type': 'application/json',
    'Accept': 'application/json',
    'Authorization': `Bearer ${this.globalService.loggedUser.value.token}`
  }


  requestOptions = {
    headers: new HttpHeaders(this.headerDict),
  };

  listProducts() {
    const url = `${this.baseUrlApi}/product`;
    return axios.get(url, { headers: this.headerDict });
  }

}
