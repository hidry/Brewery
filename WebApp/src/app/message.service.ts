import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  messages: string[] = [];

  add(message: string) {
    this.messages.push(new Date().toLocaleTimeString() + ' - ' + message);
  }

  clear() {
    this.messages = [];
  }
}
