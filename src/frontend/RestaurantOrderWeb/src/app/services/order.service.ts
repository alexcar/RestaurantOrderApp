import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { TimeOfDay } from './timeOfDay';
import { Order } from './order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44365/api/order';

  getTimeOfDay(): Observable<any> {
    return this.http.get<TimeOfDay[]>(this.url + '/getTimeOfDay').pipe(
      retry(3), catchError(this.handleError<TimeOfDay[]>('getTimeOfDay'))
    );
  }

  sendOrder(orderRequest: Order): Observable<any> {
    return this.http.post<Order>(this.url, orderRequest)
      .pipe(
        catchError(this.handleError('sendOrder', Order))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    }
  }

  private log(message: string) {
    console.log(message);
  }
}
