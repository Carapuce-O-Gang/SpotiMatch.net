import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Register } from '@models/register.model';
import { Login } from '@models/login.model';
import { User } from '@models/user';
import { Auth } from '@models/auth.model';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ApiService {
  headers: HttpHeaders;
  client: HttpClient;

  constructor(private http: HttpClient) {
    this.client = http;
    const token = localStorage.getItem('token');
    this.headers = new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: token ? `Bearer ${token}` : ""
    });
  }

  public getUser(): Promise<User> {
    return new Promise<User>((resolve, reject) => {
      const url = `${environment.apiBaseUrl}/user`;
      this.http.get(url, { headers: this.headers }).subscribe({
        next: (user) => resolve(user as User),
        error: (error) => reject(error)
      });
    });
  }

  public login(login: Login): Promise<Auth> {
    return new Promise<Auth>((resolve, reject) => {
      const url = `${environment.apiBaseUrl}/login`;
      this.http.post<Auth>(url, login, { headers: this.headers }).subscribe({
        next: (auth) => resolve(auth as Auth),
        error: (error) => reject(error)
      });
    });
  }

  public register(register: Register): Promise<User>{
    return new Promise<User>((resolve, reject) => {
      const url = `${environment.apiBaseUrl}/register`;
      this.http.post<User>(url, register, { headers: this.headers }).subscribe({
        next: (user) => resolve(user as User),
        error: (error) => reject(error)
      });
    });
  }
}
