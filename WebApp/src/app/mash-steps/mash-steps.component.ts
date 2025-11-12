import { Component, OnInit } from '@angular/core';
import { MashStep } from '../mashStep';
import { MashStepsService } from '../mash-steps.service';
import { forkJoin, of, interval } from 'rxjs';

@Component({
  selector: 'app-mash-steps',
  standalone: false,
  templateUrl: './mash-steps.component.html',
  styleUrls: ['./mash-steps.component.css']
})
export class MashStepsComponent implements OnInit {

  // gridApi and columnApi
  private api;
  private columnApi;

  mashSteps: MashStep[] = [];
  trueFalseValues: string[] = ['true', 'false'];
  trueFalseCellEditorParams = { values: this.trueFalseValues };

  columnDefs = [
    { headerName: 'Schritt', field: 'step', editable: true, checkboxSelection: true },
    {
      headerName: 'Rührwerk',
      field: 'mixer',
      editable: true,
      cellEditor: 'agSelectCellEditor',
      cellEditorParams: { values: this.trueFalseValues },
      valueFormatter: (params) => params.value ? 'true' : 'false',
      valueParser: (params) => params.newValue === 'true'
    },
    {
      headerName: 'Alarm',
      field: 'alert',
      editable: true,
      cellEditor: 'agSelectCellEditor',
      cellEditorParams: { values: this.trueFalseValues },
      valueFormatter: (params) => params.value ? 'true' : 'false',
      valueParser: (params) => params.newValue === 'true'
    },
    { headerName: 'Temperatur', field: 'temperature', editable: true, valueParser: 'Number(newValue)' },
    { headerName: 'Rast', field: 'rast', editable: true, valueParser: 'Number(newValue)' }
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
    newMashStep.step = 'new step';
    newMashStep.active = false;
    newMashStep.rast = 60;
    this.mashStepsService.addMashStep(newMashStep).subscribe(r => this.getMashSteps());
  }
}
