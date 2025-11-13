import { Component, OnInit, OnDestroy } from '@angular/core';
import { BoilingPlate1Service } from '../boiling-plate1.service';
import { SignalRService } from '../signalr.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-boiling-plate1',
  standalone: false,
  templateUrl: './boiling-plate1.component.html',
  styleUrls: ['./boiling-plate1.component.css']
})
export class BoilingPlate1Component implements OnInit, OnDestroy {

  TemperatureCurrent: number;
  CurrentStepName: string;
  CurrentStepEstimatedRemainingTime: number;
  TotalEstimatedRemainingTime: number;
  Power: boolean;

  private currentStepSubscription: Subscription;
  private totalEstimatedRemainingTimeSubscription: Subscription;
  private currentTemperatureSubscription: Subscription;
  private powerStatusSubscription: Subscription;

  constructor(
    private boilingPlate1Service: BoilingPlate1Service,
    private signalRService: SignalRService) { }

  ngOnInit() {
    // Subscribe to SignalR real-time updates instead of polling
    this.currentStepSubscription = this.signalRService.currentStep$
      .subscribe(mashStep => {
        if (mashStep) {
          this.CurrentStepName = mashStep.step;
          this.CurrentStepEstimatedRemainingTime = mashStep.estimatedTime;
        }
      });

    this.totalEstimatedRemainingTimeSubscription = this.signalRService.totalEstimatedRemainingTime$
      .subscribe(t => this.TotalEstimatedRemainingTime = Math.round(t));

    this.currentTemperatureSubscription = this.signalRService.currentTemperature$
      .subscribe(ct => this.TemperatureCurrent = Math.round(ct));

    this.powerStatusSubscription = this.signalRService.powerStatus$
      .subscribe(p => this.Power = p);
  }

  ngOnDestroy() {
    this.powerStatusSubscription?.unsubscribe();
    this.currentTemperatureSubscription?.unsubscribe();
    this.totalEstimatedRemainingTimeSubscription?.unsubscribe();
    this.currentStepSubscription?.unsubscribe();
  }

  // getPowerStatus(): void {
  //   this.boilingPlate1Service.getPowerStatus().subscribe(p => this.Power = p);
  // }

  // getCurrentTemperature(): void {
  //   this.boilingPlate1Service.getCurrentTemperature().subscribe(ct => this.TemperatureCurrent = ct);
  // }

  // getTotalEstimatedRemainingTime(): void {
  //   this.mashStepsService.getTotalEstimatedRemainingTime()
  //     .subscribe(t => this.TotalEstimatedRemainingTime = t);
  // }

  // getCurrentStep(): void {
  //   this.mashStepsService.getCurrentMashStep()
  //     .subscribe(mashStep => {
  //       this.CurrentStepName = mashStep.step;
  //       const elapsedTime = mashStep.ElapsedMinutes === undefined ? mashStep.rast : mashStep.ElapsedMinutes;
  //       this.CurrentStepEstimatedRemainingTime = mashStep.rast - elapsedTime;
  //     });
  // }

  async onPowerToggleChange(event) {
    if (event.checked) {
      await this.signalRService.startMashProcess();
    } else {
      await this.signalRService.stopMashProcess();
    }
  }

  async acknowledgeMessage() {
    await this.signalRService.acknowledgeMessage();
  }
}
