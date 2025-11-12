import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Settings } from './settings';

@Injectable({
  providedIn: 'root'
})
export class SignalRBoilingPlate2Service {
  private hubConnection: signalR.HubConnection;
  private powerStatusSubject = new BehaviorSubject<boolean>(false);
  private currentTemperatureSubject = new BehaviorSubject<number>(0);
  private temperatureSetpointSubject = new BehaviorSubject<number>(0);

  public powerStatus$ = this.powerStatusSubject.asObservable();
  public currentTemperature$ = this.currentTemperatureSubject.asObservable();
  public temperatureSetpoint$ = this.temperatureSetpointSubject.asObservable();

  constructor(private settings: Settings) {
    this.startConnection();
    this.registerEventHandlers();
  }

  private startConnection() {
    const hubUrl = this.settings.ApiUrl.replace('/api', '') + '/hubs/boilingPlate2';

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('BoilingPlate2 SignalR connection started');
        this.fetchInitialData();
      })
      .catch(err => console.error('Error while starting BoilingPlate2 SignalR connection: ' + err));
  }

  private registerEventHandlers() {
    this.hubConnection.on('PowerStatusUpdated', (powerStatus: boolean) => {
      this.powerStatusSubject.next(powerStatus);
    });

    this.hubConnection.on('CurrentTemperatureUpdated', (temperature: number) => {
      this.currentTemperatureSubject.next(temperature);
    });

    this.hubConnection.on('TemperatureSetpointUpdated', (temperature: number) => {
      this.temperatureSetpointSubject.next(temperature);
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

    this.hubConnection.invoke('GetTemperature')
      .then((temperature: number) => this.temperatureSetpointSubject.next(temperature))
      .catch(err => console.error('Error fetching temperature setpoint: ' + err));
  }

  public setPower(on: boolean): Promise<void> {
    return this.hubConnection.invoke('SetPower', on);
  }

  public setTemperature(temperature: number): Promise<void> {
    return this.hubConnection.invoke('SetTemperature', temperature);
  }

  public disconnect() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
