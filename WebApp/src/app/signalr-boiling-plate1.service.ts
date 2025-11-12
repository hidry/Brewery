import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { Settings } from './settings';

@Injectable({
  providedIn: 'root'
})
export class SignalRBoilingPlate1Service {
  private hubConnection: signalR.HubConnection;
  private powerStatusSubject = new BehaviorSubject<boolean>(false);
  private currentTemperatureSubject = new BehaviorSubject<number>(0);
  private currentStepSubject = new BehaviorSubject<{ Step: string, EstimatedTime: number }>({ Step: '', EstimatedTime: 0 });

  public powerStatus$ = this.powerStatusSubject.asObservable();
  public currentTemperature$ = this.currentTemperatureSubject.asObservable();
  public currentStep$ = this.currentStepSubject.asObservable();

  constructor(private settings: Settings) {
    this.startConnection();
    this.registerEventHandlers();
  }

  private startConnection() {
    const hubUrl = this.settings.ApiUrl.replace('/api', '') + '/hubs/boilingPlate1';

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('BoilingPlate1 SignalR connection started');
        this.fetchInitialData();
      })
      .catch(err => console.error('Error while starting BoilingPlate1 SignalR connection: ' + err));
  }

  private registerEventHandlers() {
    this.hubConnection.on('PowerStatusUpdated', (powerStatus: boolean) => {
      this.powerStatusSubject.next(powerStatus);
    });

    this.hubConnection.on('CurrentTemperatureUpdated', (temperature: number) => {
      this.currentTemperatureSubject.next(temperature);
    });

    this.hubConnection.on('CurrentStepUpdated', (data: { Step: string, EstimatedTime: number }) => {
      this.currentStepSubject.next(data);
    });
  }

  private fetchInitialData() {
    // Fetch initial data when connection is established
    this.hubConnection.invoke('GetPowerStatus')
      .then((powerStatus: boolean) => this.powerStatusSubject.next(powerStatus))
      .catch(err => console.error('Error fetching initial power status: ' + err));

    this.hubConnection.invoke('GetCurrentTemperature')
      .then((temperature: number) => this.currentTemperatureSubject.next(temperature))
      .catch(err => console.error('Error fetching initial temperature: ' + err));
  }

  public startMashProcess(): Promise<void> {
    return this.hubConnection.invoke('StartMashProcess');
  }

  public stopMashProcess(): Promise<void> {
    return this.hubConnection.invoke('StopMashProcess');
  }

  public acknowledgeMessage(): Promise<void> {
    return this.hubConnection.invoke('AcknowledgeMessage');
  }

  public disconnect() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
