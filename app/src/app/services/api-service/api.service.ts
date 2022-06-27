import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Register } from '@models/register.model';
import { Login } from '@models/login.model';
import { User } from '@models/user';
import { Auth } from '@models/auth.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
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

  public getUser(): Observable<User> {
    const url = `${environment.apiBaseUrl}/user`;
    return this.http.get<User>(url, { headers: this.headers });
  }


  public getFriendList(): Observable<User[]> {
    const url = `${environment.apiBaseUrl}/friendlist`;
    return this.http.get<User[]>(url, { headers: this.headers });
  }

  public login(login: Login): Observable<Auth> {
    const url = `${environment.apiBaseUrl}/login`;
    return this.http.post<Auth>(url, login, { headers: this.headers });
  }

  public register(register: Register): Observable<User>{
    const url = `${environment.apiBaseUrl}/register`;
    return this.http.post<User>(url, register, { headers: this.headers });
  }
}
