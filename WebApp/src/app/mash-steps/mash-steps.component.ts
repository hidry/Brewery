import { Component, OnInit } from '@angular/core';
import { MashStep } from '../mashStep';
import { MashStepsService } from '../mash-steps.service';
import { forkJoin, of, interval } from 'rxjs';

@Component({
  selector: 'app-mash-steps',
  templateUrl: './mash-steps.component.html',
  styleUrls: ['./mash-steps.component.css']
})
export class MashStepsComponent implements OnInit {

  // gridApi and columnApi
  private api;
  private columnApi;

  mashSteps: MashStep[];
  trueFalseValues: string[] = ['true', 'false'];
  trueFalseCellEditorParams = { values: this.trueFalseValues };

  columnDefs = [
    { headerName: 'Schritt', field: 'Step', editable: true, checkboxSelection: true },
    { headerName: 'Aktiv', field: 'Active', editable: true, cellEditor: 'select', cellEditorParams: this.trueFalseCellEditorParams },
    { headerName: 'Rührwerk', field: 'Mixer', editable: true, cellEditor: 'select', cellEditorParams: this.trueFalseCellEditorParams },
    { headerName: 'Alarm', field: 'Alert', editable: true, cellEditor: 'select', cellEditorParams: this.trueFalseCellEditorParams },
    { headerName: 'Rast', field: 'Rast', editable: true, valueParser: 'Number(newValue)' },
    { headerName: 'Temperatur', field: 'Temperature', editable: true, valueParser: 'Number(newValue)' }
  ];

  constructor(private mashStepsService: MashStepsService) { }

  ngOnInit() {
    this.getMashSteps();
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
    this.mashStepsService.getMashSteps()
      .subscribe(mashSteps => this.mashSteps = mashSteps);
  }

  onCellValueChanged(params: any) {
    this.mashStepsService.updateMashStep(params.data)
      .subscribe(
        ms => {
          // console.log('MashStep Saved');
          this.getMashSteps();
        },
        error => console.log(error)
      );
  }

  deleteSelectedRows() {
    const selectRows = this.api.getSelectedRows();
    // create an Observable for each row to delete
    const deleteSubscriptions = selectRows.map((rowToDelete) => {
      return this.mashStepsService.deleteMashStep(rowToDelete);
    });
    // then subscribe to these and once all done, refresh the grid data
    // deleteSubscriptions.forkJoin().subscribe(results => this.getMashSteps());

    forkJoin(...deleteSubscriptions)
      .subscribe(results => this.getMashSteps());
  }

  addNewMashStep() {
    const newMashStep = new MashStep();
    newMashStep.Step = 'new step';
    newMashStep.Active = false;
    newMashStep.Rast = 60;
    this.mashStepsService.addMashStep(newMashStep).subscribe(r => this.getMashSteps());
  }
}
