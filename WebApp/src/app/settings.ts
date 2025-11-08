import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
    providedIn: 'root',
  })
export class Settings {
    ApiUrl = environment.apiUrl;
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
    pollingInterval = 5000;
    clientLogActive = false;
}
