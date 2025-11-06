import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MashStepsComponent } from './mash-steps/mash-steps.component';
import { BoilingPlate2Component } from './boiling-plate2/boiling-plate2.component';
import { BoilingPlate1Component } from './boiling-plate1/boiling-plate1.component';
import { MessagesComponent } from './messages/messages.component';

const routes: Routes = [
  { path: '', component: BoilingPlate1Component, pathMatch: 'full' },
  { path: 'log', component: MessagesComponent },
  { path: 'mashSteps', component: MashStepsComponent },
  { path: 'boilingPlate1', component: BoilingPlate1Component },
  { path: 'boilingPlate2', component: BoilingPlate2Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
