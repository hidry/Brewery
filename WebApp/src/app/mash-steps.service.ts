import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { MashStep } from './mashStep';
import { Settings } from './settings';
import { ServiceBase } from './serviceBase';

@Injectable({
  providedIn: 'root'
})
export class MashStepsService extends ServiceBase {

  private endpointUrl = `${this.settings.ApiUrl}/mashSteps`;

  constructor(
    private http: HttpClient,
    messageService: MessageService,
    settings: Settings) {
    super(messageService, settings);
  }

  /** GET mashSteps from the server */
  getMashSteps(): Observable<MashStep[]> {
    return this.http.get<MashStep[]>(this.endpointUrl)
      .pipe(
        tap(_ => this.log('fetched mashSteps')),
        catchError(this.handleError('getMashSteps', []))
      );
  }

  /** GET currentMashStep from the server */
  getCurrentMashStep(): Observable<MashStep> {
    const url = `${this.endpointUrl}/currentStep`;
    return this.http.get<MashStep>(url)
      .pipe(
        tap(_ => this.log('fetched currentStep')),
        catchError(this.handleError<MashStep>('getCurrentMashStep'))
      );
  }

  /** GET totalEstimatedRemainingTime from the server */
  getTotalEstimatedRemainingTime(): Observable<number> {
    const url = `${this.endpointUrl}/totalEstimatedRemainingTime`;
    return this.http.get<number>(url)
      .pipe(
        tap(_ => this.log('fetched totalEstimatedRemainingTime')),
        catchError(this.handleError<number>('getTotalEstimatedRemainingTime'))
      );
  }

  updateMashStep(mashStep: MashStep): Observable<any> {
    return this.http.put(this.endpointUrl, mashStep, this.settings.httpOptions).pipe(
      tap(_ => this.log(`updated mashStep guid=${mashStep.Guid}`)),
      catchError(this.handleError<any>('updateMashStep'))
    );
  }

  /** DELETE: delete the mashStep from the server */
  deleteMashStep(mashStep: MashStep): Observable<MashStep> {
    const url = `${this.endpointUrl}/${mashStep.Guid}`;
    return this.http.delete<MashStep>(url, this.settings.httpOptions).pipe(
      tap(_ => this.log(`deleted mashStep guid=${mashStep.Guid}`)),
      catchError(this.handleError<MashStep>('deleteMashStep'))
    );
  }

  /** POST: add a new MashStep to the server */
  addMashStep(mashStep: MashStep): Observable<MashStep> {
    return this.http.post<MashStep>(this.endpointUrl, mashStep, this.settings.httpOptions).pipe(
      tap((ms: MashStep) => this.log(`added mashStep guid=${ms.Guid}`)),
      catchError(this.handleError<MashStep>('addMashStep'))
    );
  }
}
