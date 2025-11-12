import { Component, OnInit, OnDestroy } from '@angular/core';
import { BoilingPlate2Service } from '../boiling-plate2.service';
import { interval } from 'rxjs';
import { Settings } from '../settings';
import { startWith, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-boiling-plate2',
  standalone: false,
  templateUrl: './boiling-plate2.component.html',
  styleUrls: ['./boiling-plate2.component.css']
})
export class BoilingPlate2Component implements OnInit, OnDestroy {

  Temperature: number;
  TemperatureCurrent: number;
  Power: boolean;
  powerStatusSubscription: any;
  currentTemperatureSubscription: any;
  temperatureSubscription: any;
  isUserChangingTemperature: boolean = false;

  constructor(private boilingPlate2Service: BoilingPlate2Service, private settings: Settings) { }

  ngOnInit() {
    // this.getTemperature();
    this.temperatureSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.boilingPlate2Service.getTemperature())
      )
      .subscribe(t => {
        if (!this.isUserChangingTemperature) {
          this.Temperature = Math.round(t);
        }
      });

    // this.getCurrentTemperature();
    this.currentTemperatureSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.boilingPlate2Service.getCurrentTemperature())
      )
      .subscribe(ct => this.TemperatureCurrent = Math.round(ct));

    // this.getPowerStatus();
    this.powerStatusSubscription = interval(this.settings.pollingInterval)
      .pipe(
        startWith(0),
        switchMap(() => this.boilingPlate2Service.getPowerStatus())
      )
      .subscribe(p => this.Power = p);
  }

  ngOnDestroy() {
    this.powerStatusSubscription.unsubscribe();
    this.currentTemperatureSubscription.unsubscribe();
    this.temperatureSubscription.unsubscribe();
  }

  // getTemperature() {
  //   this.boilingPlate2Service.getTemperature().subscribe(t => this.Temperature = t);
  // }

  // getPowerStatus(): void {
  //   this.boilingPlate2Service.getPowerStatus().subscribe(p => this.Power = p);
  // }

  // getCurrentTemperature(): void {
  //   this.boilingPlate2Service.getCurrentTemperature().subscribe(ct => this.TemperatureCurrent = ct);
  // }

  onPowerToggleChange(event) {
    this.boilingPlate2Service.power(event.checked).subscribe();
  }

  onTemperatureSliderChange(event) {
    this.isUserChangingTemperature = true;
    // For dragStart event, the value is already in this.Temperature due to ngModel binding
  }

  onTemperatureSliderChangeEnd(event) {
    // For dragEnd event, the value is already in this.Temperature due to ngModel binding
    this.boilingPlate2Service.setTemperature(this.Temperature).subscribe(() => {
      this.isUserChangingTemperature = false;
    });
  }
}
