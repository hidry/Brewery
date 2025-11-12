import { Component, OnInit, OnDestroy } from '@angular/core';
import { MashStepsService } from '../mash-steps.service';
import { BoilingPlate1Service } from '../boiling-plate1.service';
import { interval } from 'rxjs';
import { startWith, switchMap } from 'rxjs/operators';
import { Settings } from '../settings';

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
  currentStepSubscription: any;
  totalEstimatedRemainingTimeSubscription: any;
  currentTemperatureSubscription: any;
  powerStatusSubscription: any;

  constructor(
    private mashStepsService: MashStepsService,
    private boilingPlate1Service: BoilingPlate1Service,
    private settings: Settings) { }

  ngOnInit() {

    // this.getCurrentStep();
    this.currentStepSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.mashStepsService.getCurrentMashStep()
        )
      )
      .subscribe(mashStep => {
        this.CurrentStepName = mashStep.step;
        this.CurrentStepEstimatedRemainingTime = mashStep.estimatedTime;
      });

    // this.getTotalEstimatedRemainingTime();
    this.totalEstimatedRemainingTimeSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.mashStepsService.getTotalEstimatedRemainingTime())
      )
      .subscribe(t => this.TotalEstimatedRemainingTime = Math.round(t));


    // this.getCurrentTemperature();
    this.currentTemperatureSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.boilingPlate1Service.getCurrentTemperature())
      )
      .subscribe(ct => this.TemperatureCurrent = Math.round(ct));

    // this.getPowerStatus();
    this.powerStatusSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.boilingPlate1Service.getPowerStatus())
      )
      .subscribe(p => this.Power = p);
  }

  ngOnDestroy() {
    this.powerStatusSubscription.unsubscribe();
    this.currentTemperatureSubscription.unsubscribe();
    this.totalEstimatedRemainingTimeSubscription.unsubscribe();
    this.currentStepSubscription.unsubscribe();
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

  onPowerToggleChange(event) {
    if (event.checked) {
      this.boilingPlate1Service.start().subscribe();
    } else {
      this.boilingPlate1Service.stop().subscribe();
    }
  }

  acknowledgeMessage() {
    this.boilingPlate1Service.acknowledgeMessage().subscribe();
  }
}
