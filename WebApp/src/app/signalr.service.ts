import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { Settings } from './settings';
import { MashStep } from './mashStep';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private boilingPlate1Connection: signalR.HubConnection;
  private boilingPlate2Connection: signalR.HubConnection;
  private mashStepsConnection: signalR.HubConnection;

  // Observables for BoilingPlate1 real-time data
  private powerStatusSubject = new BehaviorSubject<boolean>(false);
  private currentTemperatureSubject = new BehaviorSubject<number>(0);
  private currentStepSubject = new BehaviorSubject<MashStep | null>(null);
  private totalEstimatedRemainingTimeSubject = new BehaviorSubject<number>(0);

  // Observables for BoilingPlate2 real-time data
  private boilingPlate2PowerStatusSubject = new BehaviorSubject<boolean>(false);
  private boilingPlate2CurrentTemperatureSubject = new BehaviorSubject<number>(0);
  private boilingPlate2TemperatureSetpointSubject = new BehaviorSubject<number>(0);

  // Observables for MashSteps CRUD operations
  private mashStepsSubject = new BehaviorSubject<MashStep[]>([]);

  public powerStatus$ = this.powerStatusSubject.asObservable();
  public currentTemperature$ = this.currentTemperatureSubject.asObservable();
  public currentStep$ = this.currentStepSubject.asObservable();
  public totalEstimatedRemainingTime$ = this.totalEstimatedRemainingTimeSubject.asObservable();

  public boilingPlate2PowerStatus$ = this.boilingPlate2PowerStatusSubject.asObservable();
  public boilingPlate2CurrentTemperature$ = this.boilingPlate2CurrentTemperatureSubject.asObservable();
  public boilingPlate2TemperatureSetpoint$ = this.boilingPlate2TemperatureSetpointSubject.asObservable();

  public mashSteps$ = this.mashStepsSubject.asObservable();

  constructor(private settings: Settings) {
    this.initializeConnections();
  }

  private getHubUrl(hubPath: string): string {
    const apiUrl = this.settings.ApiUrl;
    // Remove /api from the end if present, then add the hub path
    const baseUrl = apiUrl.endsWith('/api') ? apiUrl.slice(0, -4) : apiUrl;
    return `${baseUrl}${hubPath}`;
  }

  private initializeConnections(): void {
    // Initialize BoilingPlate1 Hub connection
    this.boilingPlate1Connection = new signalR.HubConnectionBuilder()
      .withUrl(this.getHubUrl('/hubs/boilingPlate1'))
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    // Initialize BoilingPlate2 Hub connection
    this.boilingPlate2Connection = new signalR.HubConnectionBuilder()
      .withUrl(this.getHubUrl('/hubs/boilingPlate2'))
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    // Initialize MashSteps Hub connection
    this.mashStepsConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.getHubUrl('/hubs/mashSteps'))
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    // Set up event handlers for BoilingPlate1 Hub
    this.boilingPlate1Connection.on('PowerStatusUpdated', (powerStatus: boolean) => {
      this.powerStatusSubject.next(powerStatus);
    });

    this.boilingPlate1Connection.on('CurrentTemperatureUpdated', (temperature: number) => {
      this.currentTemperatureSubject.next(temperature);
    });

    // Set up event handlers for BoilingPlate2 Hub
    this.boilingPlate2Connection.on('PowerStatusUpdated', (powerStatus: boolean) => {
      this.boilingPlate2PowerStatusSubject.next(powerStatus);
    });

    this.boilingPlate2Connection.on('CurrentTemperatureUpdated', (temperature: number) => {
      this.boilingPlate2CurrentTemperatureSubject.next(temperature);
    });

    this.boilingPlate2Connection.on('TemperatureSetpointUpdated', (temperature: number) => {
      this.boilingPlate2TemperatureSetpointSubject.next(temperature);
    });

    // Set up event handlers for MashSteps Hub
    this.mashStepsConnection.on('CurrentStepUpdated', (currentStep: MashStep) => {
      this.currentStepSubject.next(currentStep);
    });

    this.mashStepsConnection.on('TotalEstimatedRemainingTimeUpdated', (totalTime: number) => {
      this.totalEstimatedRemainingTimeSubject.next(totalTime);
    });

    // Event handlers for MashSteps CRUD operations
    this.mashStepsConnection.on('MashStepsChanged', async () => {
      // When MashSteps change, fetch the updated list
      await this.refreshMashSteps();
    });

    this.mashStepsConnection.on('MashStepUpdated', async (mashStep: MashStep) => {
      // Update the specific step in the local list
      const currentSteps = this.mashStepsSubject.value;
      const index = currentSteps.findIndex(ms => ms.guid === mashStep.guid);
      if (index !== -1) {
        currentSteps[index] = mashStep;
        this.mashStepsSubject.next([...currentSteps]);
      }
    });

    this.mashStepsConnection.on('MashStepDeleted', (guid: string) => {
      // Remove the deleted step from the local list
      const currentSteps = this.mashStepsSubject.value;
      const filteredSteps = currentSteps.filter(ms => ms.guid !== guid);
      this.mashStepsSubject.next(filteredSteps);
    });

    this.mashStepsConnection.on('MashStepInserted', (mashStep: MashStep) => {
      // Add the new step to the local list
      const currentSteps = this.mashStepsSubject.value;
      this.mashStepsSubject.next([...currentSteps, mashStep]);
    });

    // Start connections
    this.startConnections();
  }

  private async startConnections(): Promise<void> {
    try {
      await this.boilingPlate1Connection.start();
      console.log('BoilingPlate1 Hub connection started');

      // Fetch initial values
      await this.fetchInitialBoilingPlate1Values();
    } catch (err) {
      console.error('Error starting BoilingPlate1 Hub connection:', err);
      // Retry after 5 seconds
      setTimeout(() => this.startBoilingPlate1Connection(), 5000);
    }

    try {
      await this.boilingPlate2Connection.start();
      console.log('BoilingPlate2 Hub connection started');

      // Fetch initial values
      await this.fetchInitialBoilingPlate2Values();
    } catch (err) {
      console.error('Error starting BoilingPlate2 Hub connection:', err);
      // Retry after 5 seconds
      setTimeout(() => this.startBoilingPlate2Connection(), 5000);
    }

    try {
      await this.mashStepsConnection.start();
      console.log('MashSteps Hub connection started');

      // Fetch initial values
      await this.fetchInitialMashStepsValues();
    } catch (err) {
      console.error('Error starting MashSteps Hub connection:', err);
      // Retry after 5 seconds
      setTimeout(() => this.startMashStepsConnection(), 5000);
    }
  }

  private async startBoilingPlate1Connection(): Promise<void> {
    try {
      await this.boilingPlate1Connection.start();
      console.log('BoilingPlate1 Hub connection started');
      await this.fetchInitialBoilingPlate1Values();
    } catch (err) {
      console.error('Error starting BoilingPlate1 Hub connection:', err);
      setTimeout(() => this.startBoilingPlate1Connection(), 5000);
    }
  }

  private async startBoilingPlate2Connection(): Promise<void> {
    try {
      await this.boilingPlate2Connection.start();
      console.log('BoilingPlate2 Hub connection started');
      await this.fetchInitialBoilingPlate2Values();
    } catch (err) {
      console.error('Error starting BoilingPlate2 Hub connection:', err);
      setTimeout(() => this.startBoilingPlate2Connection(), 5000);
    }
  }

  private async startMashStepsConnection(): Promise<void> {
    try {
      await this.mashStepsConnection.start();
      console.log('MashSteps Hub connection started');
      await this.fetchInitialMashStepsValues();
    } catch (err) {
      console.error('Error starting MashSteps Hub connection:', err);
      setTimeout(() => this.startMashStepsConnection(), 5000);
    }
  }

  private async fetchInitialBoilingPlate1Values(): Promise<void> {
    try {
      const powerStatus = await this.boilingPlate1Connection.invoke<boolean>('GetPowerStatus');
      this.powerStatusSubject.next(powerStatus);

      const temperature = await this.boilingPlate1Connection.invoke<number>('GetCurrentTemperature');
      this.currentTemperatureSubject.next(temperature);
    } catch (err) {
      console.error('Error fetching initial values from BoilingPlate1 Hub:', err);
    }
  }

  private async fetchInitialBoilingPlate2Values(): Promise<void> {
    try {
      const powerStatus = await this.boilingPlate2Connection.invoke<boolean>('GetPowerStatus');
      this.boilingPlate2PowerStatusSubject.next(powerStatus);

      const temperature = await this.boilingPlate2Connection.invoke<number>('GetCurrentTemperature');
      this.boilingPlate2CurrentTemperatureSubject.next(temperature);

      const setpoint = await this.boilingPlate2Connection.invoke<number>('GetTemperature');
      this.boilingPlate2TemperatureSetpointSubject.next(setpoint);
    } catch (err) {
      console.error('Error fetching initial values from BoilingPlate2 Hub:', err);
    }
  }

  private async fetchInitialMashStepsValues(): Promise<void> {
    try {
      const currentStep = await this.mashStepsConnection.invoke<MashStep>('GetCurrentMashStep');
      this.currentStepSubject.next(currentStep);

      const totalTime = await this.mashStepsConnection.invoke<number>('GetTotalEstimatedRemainingTime');
      this.totalEstimatedRemainingTimeSubject.next(totalTime);

      const mashSteps = await this.mashStepsConnection.invoke<MashStep[]>('GetMashSteps');
      this.mashStepsSubject.next(mashSteps);
    } catch (err) {
      console.error('Error fetching initial values from MashSteps Hub:', err);
    }
  }

  private async refreshMashSteps(): Promise<void> {
    try {
      if (this.mashStepsConnection.state === signalR.HubConnectionState.Connected) {
        const mashSteps = await this.mashStepsConnection.invoke<MashStep[]>('GetMashSteps');
        this.mashStepsSubject.next(mashSteps);
      }
    } catch (err) {
      console.error('Error refreshing mash steps:', err);
    }
  }

  // Methods to invoke BoilingPlate1 server methods
  public async startMashProcess(): Promise<void> {
    if (this.boilingPlate1Connection.state === signalR.HubConnectionState.Connected) {
      await this.boilingPlate1Connection.invoke('StartMashProcess');
    }
  }

  public async stopMashProcess(): Promise<void> {
    if (this.boilingPlate1Connection.state === signalR.HubConnectionState.Connected) {
      await this.boilingPlate1Connection.invoke('StopMashProcess');
    }
  }

  public async acknowledgeMessage(): Promise<void> {
    if (this.boilingPlate1Connection.state === signalR.HubConnectionState.Connected) {
      await this.boilingPlate1Connection.invoke('AcknowledgeMessage');
    }
  }

  // Methods to invoke BoilingPlate2 server methods
  public async setBoilingPlate2Power(on: boolean): Promise<void> {
    if (this.boilingPlate2Connection.state === signalR.HubConnectionState.Connected) {
      await this.boilingPlate2Connection.invoke('SetPower', on);
    }
  }

  public async setBoilingPlate2Temperature(temperature: number): Promise<void> {
    if (this.boilingPlate2Connection.state === signalR.HubConnectionState.Connected) {
      await this.boilingPlate2Connection.invoke('SetTemperature', temperature);
    }
  }

  // Methods to invoke MashSteps server methods
  public async updateMashStep(mashStep: MashStep): Promise<void> {
    if (this.mashStepsConnection.state === signalR.HubConnectionState.Connected) {
      await this.mashStepsConnection.invoke('UpdateMashStep', mashStep);
    }
  }

  public async deleteMashStep(guid: string): Promise<void> {
    if (this.mashStepsConnection.state === signalR.HubConnectionState.Connected) {
      await this.mashStepsConnection.invoke('DeleteMashStep', guid);
    }
  }

  public async insertMashStep(mashStep: MashStep): Promise<MashStep> {
    if (this.mashStepsConnection.state === signalR.HubConnectionState.Connected) {
      return await this.mashStepsConnection.invoke<MashStep>('InsertMashStep', mashStep);
    }
    return mashStep;
  }

  // Cleanup
  public disconnect(): void {
    this.boilingPlate1Connection?.stop();
    this.boilingPlate2Connection?.stop();
    this.mashStepsConnection?.stop();
  }
}
