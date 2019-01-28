import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { MessagesComponent } from './messages/messages.component';
import { HttpClientModule } from '@angular/common/http';

import { AgGridModule } from 'ag-grid-angular';
import { MashStepsComponent } from './mash-steps/mash-steps.component';
import { BoilingPlate2Component } from './boiling-plate2/boiling-plate2.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule, MatIconModule, MatSlideToggleModule, MatSliderModule, MatCheckboxModule } from '@angular/material';
import { BoilingPlate1Component } from './boiling-plate1/boiling-plate1.component';

@NgModule({
  declarations: [
    AppComponent,
    MessagesComponent,
    MashStepsComponent,
    BoilingPlate2Component,
    BoilingPlate1Component,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AgGridModule.withComponents(null),
    BrowserAnimationsModule,
    MatCardModule, MatIconModule, MatSlideToggleModule, MatSliderModule, MatCheckboxModule
    // ,
    // // The HttpClientInMemoryWebApiModule module intercepts HTTP requests
    // // and returns simulated server responses.
    // // Remove it when a real server is ready to receive requests.
    // HttpClientInMemoryWebApiModule.forRoot(
    //   InMemoryDataService, { dataEncapsulation: false }
    // )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
