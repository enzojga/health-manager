import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private readonly apiUrl: string = "http://localhost:5189/api";

  constructor(private readonly http: HttpClient) { }

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
}
