import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

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
  
  constructor(private http: HttpClient) { 

  }

  public getUser(){
    
  }

  deleteUser(id:number): Observable<any>{
    const url = `${this.baseUrl}/${id}`;
    return this.http.delete(url, this.httpOptions).pipe(
      catchError(
        (async () => new Error('Error in Delete'))
        ));
  }

  
}
