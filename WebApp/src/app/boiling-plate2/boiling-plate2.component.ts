import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRBoilingPlate2Service } from '../signalr-boiling-plate2.service';
import { Subscription } from 'rxjs';

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
  powerStatusSubscription: Subscription;
  currentTemperatureSubscription: Subscription;
  temperatureSubscription: Subscription;
  isUserChangingTemperature: boolean = false;

  constructor(private signalRBoilingPlate2Service: SignalRBoilingPlate2Service) { }

  ngOnInit() {
    // Subscribe to SignalR real-time updates instead of polling
    this.temperatureSubscription = this.signalRBoilingPlate2Service.temperatureSetpoint$
      .subscribe(t => {
        if (!this.isUserChangingTemperature) {
          this.Temperature = Math.round(t);
        }
      });

    this.currentTemperatureSubscription = this.signalRBoilingPlate2Service.currentTemperature$
      .subscribe(ct => this.TemperatureCurrent = Math.round(ct));

    this.powerStatusSubscription = this.signalRBoilingPlate2Service.powerStatus$
      .subscribe(p => this.Power = p);
  }

  ngOnDestroy() {
    this.powerStatusSubscription.unsubscribe();
    this.currentTemperatureSubscription.unsubscribe();
    this.temperatureSubscription.unsubscribe();
  }

  onPowerToggleChange(event) {
    this.signalRBoilingPlate2Service.setPower(event.checked);
  }

  onTemperatureSliderChange(event) {
    this.isUserChangingTemperature = true;
    this.Temperature = event.value;
  }

  onTemperatureSliderChangeEnd(event) {
    this.signalRBoilingPlate2Service.setTemperature(event.value)
      .then(() => {
        this.isUserChangingTemperature = false;
      });
  }
}
