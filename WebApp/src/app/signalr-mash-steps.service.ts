import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Settings } from './settings';
import { MashStep } from './mashStep';

@Injectable({
  providedIn: 'root'
})
export class SignalRMashStepsService {
  private hubConnection: signalR.HubConnection;
  private currentStepSubject = new BehaviorSubject<MashStep>(null);
  private totalEstimatedRemainingTimeSubject = new BehaviorSubject<number>(0);
  private mashStepsChangedSubject = new BehaviorSubject<void>(null);

  public currentStep$ = this.currentStepSubject.asObservable();
  public totalEstimatedRemainingTime$ = this.totalEstimatedRemainingTimeSubject.asObservable();
  public mashStepsChanged$ = this.mashStepsChangedSubject.asObservable();

  constructor(private settings: Settings) {
    this.startConnection();
    this.registerEventHandlers();
  }

  private startConnection() {
    const hubUrl = this.settings.ApiUrl.replace('/api', '') + '/hubs/mashSteps';

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('MashSteps SignalR connection started');
        this.fetchInitialData();
      })
      .catch(err => console.error('Error while starting MashSteps SignalR connection: ' + err));
  }

  private registerEventHandlers() {
    this.hubConnection.on('CurrentStepUpdated', (currentStep: MashStep) => {
      this.currentStepSubject.next(currentStep);
    });

    this.hubConnection.on('TotalEstimatedRemainingTimeUpdated', (totalTime: number) => {
      this.totalEstimatedRemainingTimeSubject.next(totalTime);
    });

    this.hubConnection.on('MashStepsChanged', () => {
      this.mashStepsChangedSubject.next(null);
    });

    this.hubConnection.on('MashStepUpdated', () => {
      this.mashStepsChangedSubject.next(null);
    });

    this.hubConnection.on('MashStepDeleted', () => {
      this.mashStepsChangedSubject.next(null);
    });

    this.hubConnection.on('MashStepInserted', () => {
      this.mashStepsChangedSubject.next(null);
    });
  }

  private fetchInitialData() {
    // Fetch initial data when connection is established
    this.hubConnection.invoke('GetCurrentMashStep')
      .then((currentStep: MashStep) => this.currentStepSubject.next(currentStep))
      .catch(err => console.error('Error fetching initial current step: ' + err));

    this.hubConnection.invoke('GetTotalEstimatedRemainingTime')
      .then((totalTime: number) => this.totalEstimatedRemainingTimeSubject.next(totalTime))
      .catch(err => console.error('Error fetching initial total time: ' + err));
  }

  public getMashSteps(): Promise<MashStep[]> {
    return this.hubConnection.invoke('GetMashSteps');
  }

  public getCurrentMashStep(): Promise<MashStep> {
    return this.hubConnection.invoke('GetCurrentMashStep');
  }

  public getTotalEstimatedRemainingTime(): Promise<number> {
    return this.hubConnection.invoke('GetTotalEstimatedRemainingTime');
  }

  public updateMashStep(mashStep: MashStep): Promise<void> {
    return this.hubConnection.invoke('UpdateMashStep', mashStep);
  }

  public deleteMashStep(guid: string): Promise<void> {
    return this.hubConnection.invoke('DeleteMashStep', guid);
  }

  public insertMashStep(mashStep: MashStep): Promise<MashStep> {
    return this.hubConnection.invoke('InsertMashStep', mashStep);
  }

  public disconnect() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
