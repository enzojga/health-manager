import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private readonly apiUrl: string = "http://localhost:5189/api";

  constructor(private readonly http: HttpClient) { }

  private static handleError(error: HttpErrorResponse) {
    return throwError(error);
  }

  genericGet<T>(endpoint: string, id?: string): Observable<T> {
    if (id) {
      const url = `${this.apiUrl}/${endpoint}/${id}`;
      return this.http.get<T>(url)
        .pipe(map((response: T) => response));
    } else {
      const url = `${this.apiUrl}/${endpoint}`;
      return this.http.get<T>(url)
        .pipe(map((response: T) => response));
    }
  }

  genericPost<T>(endpoint: string, data: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.post<T>(url, data)
      .pipe(map((response: T) => {
        return response;
      }))
      .pipe(catchError(HttpService.handleError));
  }
  
  genericPut<T>(endpoint: string, data: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.put<T>(url, data)
      .pipe(map((response: T) => response));
  }

  genericDelete<T>(endpoint: string, id: string): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}/${id}`;
    return this.http.delete<T>(url)
      .pipe(map((response: T) => response));
  }
}
