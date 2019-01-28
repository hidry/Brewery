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
export class BoilingPlate1Service extends ServiceBase {

  private endpointUrl = `${this.settings.ApiUrl}/boilingPlate1`;

  constructor(
    private http: HttpClient,
    messageService: MessageService,
    settings: Settings) {
    super(messageService, settings);
  }

  acknowledgeMessage(): Observable<any> {
    const url = `${this.endpointUrl}/acknowledgeMessage`;
    return this.http.put(url, null, this.settings.httpOptions).pipe(
      tap(_ => this.log(`put acknowledgeMessage`)),
      catchError(this.handleError<any>('acknowledgeMessage'))
    );
  }

  start(): Observable<any> {
    const url = `${this.endpointUrl}/startMashProcess`;
    return this.http.put(url, null, this.settings.httpOptions).pipe(
      tap(_ => this.log(`put startMashProcess`)),
      catchError(this.handleError<any>('start'))
    );
  }

  stop(): Observable<any> {
    const url = `${this.endpointUrl}/stopMashProcess`;
    return this.http.put(url, null, this.settings.httpOptions).pipe(
      tap(_ => this.log(`put stopMashProcess`)),
      catchError(this.handleError<any>('stop'))
    );
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

}
