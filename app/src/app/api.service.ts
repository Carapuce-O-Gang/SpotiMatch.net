import { registerData } from './model/register.model';
import { loginData } from './model/login.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from './model/user';
import { Auth } from './model/auth.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl='';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: 'my-auth-token'
    })
  };
  client: HttpClient;
  
  constructor(private http: HttpClient) { 
    this.client = http;
  }

  getUser(): Observable<User[] | Error | object>{
    const url = `${this.baseUrl}`;
    return this.http.get(url, this.httpOptions).pipe(
      catchError(
        (async () => new Error('Error in getUserById'))
      )
    );
  }

  getUserById(id: number): Observable<User | Error | object>{
    const url = `${this.baseUrl}/${id}`;
    return this.http.get(url, this.httpOptions).pipe(
      catchError(
        (async () => new Error('Error in getUserById'))
      )
    );
  }

  login(loginData: loginData): Observable<Auth | Error | object>{
    const url = `${this.baseUrl}/login`;
    return this.http.post(url,loginData,this.httpOptions).pipe(
      catchError(
        (async () => new Error('Error in Login'))
      )
    );
  }

  register(registerData: registerData): Observable<User | Error | object>{
    const url = `${this.baseUrl}/register`;
    return this.http.post(url,registerData,this.httpOptions).pipe(
      catchError(
        (async () => new Error('Error in Register'))
      )
    );
  }

  // deleteUser(id:number): Observable<any>{
  //   const url = `${this.baseUrl}/${id}`;
  //   return this.http.delete(url, this.httpOptions).pipe(
  //     catchError(
  //       (async () => new Error('Error in Delete'))
  //     )
  //   )
  // }
  
}
