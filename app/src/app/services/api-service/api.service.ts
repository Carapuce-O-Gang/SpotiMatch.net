import { RegisterData } from '../../model/register.model';
import { LoginData } from '../../model/login.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, Observable, Subscription, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../../model/user';
import { Auth } from '../../model/auth.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl='';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')}`
    })
  };
  client: HttpClient;
  errorMessage: string;

  constructor(private http: HttpClient) { 
    this.client = http;
    this.errorMessage = '';
  }

  getUser(){
    const url = `${this.baseUrl}`;
    return this.http.get(url, {headers: this.httpOptions.headers}).subscribe(data => {
      return data;
    },
    error =>{
      this.errorMessage = error.message;
      console.error('There was an error in getUser request', error);
      return error;
    })
  }

  getUserById(id: number){
    const url = `${this.baseUrl}/${id}`;
    return this.http.get<User>(url,{headers: this.httpOptions.headers}).subscribe(data => {
      return data;
    },
    error => {
      this.errorMessage = error.message;
      console.error('There was an error in getUserById request', error);
      return error;
    })
  }

  login(LoginData: LoginData){
    const url = `${this.baseUrl}/login`;
    return this.http.post<Auth>(url,LoginData,{headers: this.httpOptions.headers}).subscribe(data => {
      if (data.token) {
        localStorage.setItem('token', data.token);
        return data;
      }
    },
    error => {
      this.errorMessage = error.message;
      console.error('There was an error in Login Request', error);
      return error;
    });
  }
  

  register(RegisterData: RegisterData){
    const url = `${this.baseUrl}/register`;
    return this.http.post<User>(url,RegisterData,{headers: this.httpOptions.headers}).subscribe(data => {
      return data
    },
    error => {
      this.errorMessage = error.message;
      console.error('There was an error in Register Request', error);
      return error;
    })
  }
  
}
