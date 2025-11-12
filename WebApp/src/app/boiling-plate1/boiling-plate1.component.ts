import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRBoilingPlate1Service } from '../signalr-boiling-plate1.service';
import { SignalRMashStepsService } from '../signalr-mash-steps.service';
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
  currentStepSubscription: Subscription;
  totalEstimatedRemainingTimeSubscription: Subscription;
  currentTemperatureSubscription: Subscription;
  powerStatusSubscription: Subscription;

  constructor(
    private signalRBoilingPlate1Service: SignalRBoilingPlate1Service,
    private signalRMashStepsService: SignalRMashStepsService) { }

  ngOnInit() {
    // Subscribe to SignalR real-time updates instead of polling
    this.currentStepSubscription = this.signalRBoilingPlate1Service.currentStep$.subscribe(data => {
      this.CurrentStepName = data.Step;
      this.CurrentStepEstimatedRemainingTime = data.EstimatedTime;
    });

    this.totalEstimatedRemainingTimeSubscription = this.signalRMashStepsService.totalEstimatedRemainingTime$
      .subscribe(t => this.TotalEstimatedRemainingTime = Math.round(t));

    this.currentTemperatureSubscription = this.signalRBoilingPlate1Service.currentTemperature$
      .subscribe(ct => this.TemperatureCurrent = Math.round(ct));

    this.powerStatusSubscription = this.signalRBoilingPlate1Service.powerStatus$
      .subscribe(p => this.Power = p);
  }

  ngOnDestroy() {
    this.powerStatusSubscription.unsubscribe();
    this.currentTemperatureSubscription.unsubscribe();
    this.totalEstimatedRemainingTimeSubscription.unsubscribe();
    this.currentStepSubscription.unsubscribe();
  }

  onPowerToggleChange(event) {
    if (event.checked) {
      this.signalRBoilingPlate1Service.startMashProcess();
    } else {
      this.signalRBoilingPlate1Service.stopMashProcess();
    }
  }

  acknowledgeMessage() {
    this.signalRBoilingPlate1Service.acknowledgeMessage();
  }
}
