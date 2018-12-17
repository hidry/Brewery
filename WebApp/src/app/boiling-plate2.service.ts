import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { catchError, map, tap } from 'rxjs/operators';
import { ServiceBase } from './serviceBase';
import { Settings } from './settings';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BoilingPlate2Service extends ServiceBase {

  private endpointUrl = `${this.settings.ApiUrl}/boilingPlate2`;

  constructor(
    private http: HttpClient,
    messageService: MessageService,
    private settings: Settings) {
    super(messageService);
  }

  getPowerStatus(): Observable<boolean> {
    const url = `${this.endpointUrl}/powerStatus`;
    return this.http.get<boolean>(url)
      .pipe(
        tap(_ => this.log('fetched PowerStatus')),
        catchError(this.handleError<boolean>('getPowerStatus'))
      );
  }

  /** GET currentTemperature from the server */
  getCurrentTemperature(): Observable<number> {
    const url = `${this.endpointUrl}/getCurrentTemperature`;
    return this.http.get<number>(url)
      .pipe(
        tap(_ => this.log('fetched currentTemperature')),
        catchError(this.handleError<number>('getCurrentTemperature'))
      );
  }

  getTemperature(): Observable<number> {
    const url = `${this.endpointUrl}/getTemperature`;
    return this.http.get<number>(url)
      .pipe(
        tap(_ => this.log('fetched Temperature')),
        catchError(this.handleError<number>('getTemperature'))
      );
  }

  power(power: boolean): Observable<any> {
    const url = `${this.endpointUrl}/power/${power}`;
    return this.http.put(url, null, this.settings.httpOptions).pipe(
      tap(_ => this.log(`put power ${power}`)),
      catchError(this.handleError<any>('power'))
    );
  }

  setTemperature(temperature: number): Observable<any> {
    const url = `${this.endpointUrl}/setTemperature/${temperature}`;
    return this.http.put(url, null, this.settings.httpOptions).pipe(
      tap(_ => this.log(`put temperature ${temperature}`)),
      catchError(this.handleError<any>('setTemperature'))
    );
  }

}
