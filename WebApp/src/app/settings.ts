import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
  })
export class Settings {
    ApiUrl = 'http://minwinpc:8800/api/';
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
    pollingInterval = 5000;
    clientLogActive = false;
}
