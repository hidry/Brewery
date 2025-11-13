import { Component, OnInit, OnDestroy } from '@angular/core';
import { MashStep } from '../mashStep';
import { SignalRService } from '../signalr.service';
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
  hasSelectedRows = false;

  private mashStepsSubscription: Subscription;

  columnDefs = [
    { headerName: 'Schritt', field: 'step', editable: true, checkboxSelection: true },
    {
      headerName: 'Rührwerk',
      field: 'mixer',
      editable: true,
      cellRenderer: 'agCheckboxCellRenderer',
      cellEditor: 'agCheckboxCellEditor'
    },
    {
      headerName: 'Alarm',
      field: 'alert',
      editable: true,
      cellRenderer: 'agCheckboxCellRenderer',
      cellEditor: 'agCheckboxCellEditor'
    },
    { headerName: 'Temperatur', field: 'temperature', editable: true, valueParser: 'Number(newValue)' },
    { headerName: 'Rast', field: 'rast', editable: true, valueParser: 'Number(newValue)' }
  ];

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    // Subscribe to SignalR real-time updates instead of polling
    this.mashStepsSubscription = this.signalRService.mashSteps$
      .subscribe(mashSteps => {
        this.mashSteps = mashSteps;
      });
  }

  ngOnDestroy() {
    this.mashStepsSubscription?.unsubscribe();
  }

  // on grid initialisation, grap the APIs and auto resize the columns to fit the available space
  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;

    this.api.sizeColumnsToFit();
  }

  onSelectionChanged(): void {
    this.hasSelectedRows = this.api && this.api.getSelectedRows().length > 0;
  }

  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }

  async onCellValueChanged(params: any) {
    try {
      await this.signalRService.updateMashStep(params.data);
      // SignalR will automatically broadcast the update to all clients
    } catch (error) {
      console.error('Error updating mash step:', error);
    }
  }

  async deleteSelectedRows() {
    const selectedRows = this.api.getSelectedRows();
    try {
      // Delete all selected rows via SignalR
      for (const rowToDelete of selectedRows) {
        await this.signalRService.deleteMashStep(rowToDelete.guid);
      }
      // SignalR will automatically broadcast the deletions to all clients
    } catch (error) {
      console.error('Error deleting mash steps:', error);
    }
  }

  async addNewMashStep() {
    const newMashStep = new MashStep();
    newMashStep.step = 'new step';
    newMashStep.active = false;
    newMashStep.rast = 60;
    try {
      await this.signalRService.insertMashStep(newMashStep);
      // SignalR will automatically broadcast the insertion to all clients
    } catch (error) {
      console.error('Error adding mash step:', error);
    }
  }
}
