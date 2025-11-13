import { Component, OnInit, OnDestroy } from '@angular/core';
import { BoilingPlate2Service } from '../boiling-plate2.service';
import { SignalRService } from '../signalr.service';
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
  isUserChangingTemperature: boolean = false;

  private powerStatusSubscription: Subscription;
  private currentTemperatureSubscription: Subscription;
  private temperatureSubscription: Subscription;

  constructor(
    private boilingPlate2Service: BoilingPlate2Service,
    private signalRService: SignalRService) { }

  ngOnInit() {
    // Subscribe to SignalR real-time updates instead of polling
    this.temperatureSubscription = this.signalRService.boilingPlate2TemperatureSetpoint$
      .subscribe(t => {
        if (!this.isUserChangingTemperature) {
          this.Temperature = Math.round(t);
        }
      });

    this.currentTemperatureSubscription = this.signalRService.boilingPlate2CurrentTemperature$
      .subscribe(ct => this.TemperatureCurrent = Math.round(ct));

    this.powerStatusSubscription = this.signalRService.boilingPlate2PowerStatus$
      .subscribe(p => this.Power = p);
  }

  ngOnDestroy() {
    this.powerStatusSubscription?.unsubscribe();
    this.currentTemperatureSubscription?.unsubscribe();
    this.temperatureSubscription?.unsubscribe();
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

  async onPowerToggleChange(event) {
    await this.signalRService.setBoilingPlate2Power(event.checked);
  }

  onTemperatureSliderChange(event) {
    this.isUserChangingTemperature = true;
    // For dragStart event, the value is already in this.Temperature due to ngModel binding
  }

  async onTemperatureSliderChangeEnd(event) {
    // For dragEnd event, the value is already in this.Temperature due to ngModel binding
    await this.signalRService.setBoilingPlate2Temperature(this.Temperature);
    this.isUserChangingTemperature = false;
  }
}
