import { Component, OnInit, OnDestroy } from '@angular/core';
import { MashStep } from '../mashStep';
import { SignalRMashStepsService } from '../signalr-mash-steps.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mash-steps',
  standalone: false,
  templateUrl: './mash-steps.component.html',
  styleUrls: ['./mash-steps.component.css']
})
export class MashStepsComponent implements OnInit, OnDestroy {

  // gridApi and columnApi
  private api;
  private columnApi;

  mashSteps: MashStep[] = [];
  trueFalseValues: string[] = ['true', 'false'];
  trueFalseCellEditorParams = { values: this.trueFalseValues };
  mashStepsChangedSubscription: Subscription;

  columnDefs = [
    { headerName: 'Schritt', field: 'Step', editable: true, checkboxSelection: true },
    { headerName: 'Rührwerk', field: 'Mixer', editable: true, cellEditor: 'select', cellEditorParams: this.trueFalseCellEditorParams },
    { headerName: 'Alarm', field: 'Alert', editable: true, cellEditor: 'select', cellEditorParams: this.trueFalseCellEditorParams },
    { headerName: 'Temperatur', field: 'Temperature', editable: true, valueParser: 'Number(newValue)' },
    { headerName: 'Rast', field: 'Rast', editable: true, valueParser: 'Number(newValue)' }
  ];

  constructor(private signalRMashStepsService: SignalRMashStepsService) { }

  ngOnInit() {
    this.getMashSteps();

    // Subscribe to SignalR real-time updates
    this.mashStepsChangedSubscription = this.signalRMashStepsService.mashStepsChanged$
      .subscribe(() => {
        this.getMashSteps();
      });
  }

  ngOnDestroy() {
    if (this.mashStepsChangedSubscription) {
      this.mashStepsChangedSubscription.unsubscribe();
    }
  }

  // on grid initialisation, grap the APIs and auto resize the columns to fit the available space
  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;

    this.api.sizeColumnsToFit();
  }

  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }

  getMashSteps(): void {
    this.signalRMashStepsService.getMashSteps()
      .then(mashSteps => this.mashSteps = mashSteps)
      .catch(error => console.error('Error loading mash steps:', error));
  }

  onCellValueChanged(params: any) {
    this.signalRMashStepsService.updateMashStep(params.data)
      .then(() => {
        // MashStep saved - data will be refreshed via SignalR
      })
      .catch(error => console.error('Error updating mash step:', error));
  }

  deleteSelectedRows() {
    const selectRows = this.api.getSelectedRows();

    // Delete each selected row via SignalR
    const deletePromises = selectRows.map((rowToDelete) => {
      return this.signalRMashStepsService.deleteMashStep(rowToDelete.Guid);
    });

    Promise.all(deletePromises)
      .then(() => {
        // Data will be refreshed via SignalR
      })
      .catch(error => console.error('Error deleting mash steps:', error));
  }

  addNewMashStep() {
    const newMashStep = new MashStep();
    newMashStep.Step = 'new step';
    newMashStep.Active = false;
    newMashStep.Rast = 60;
    this.signalRMashStepsService.insertMashStep(newMashStep)
      .then(() => {
        // Data will be refreshed via SignalR
      })
      .catch(error => console.error('Error adding mash step:', error));
  }
}
