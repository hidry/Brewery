import { MessageService } from './message.service';
import { Observable, of } from 'rxjs';
import { Settings } from './settings';

export class ServiceBase {

    constructor(protected messageService: MessageService, protected settings: Settings) {}

    /** Log a MashStepsService message with the MessageService */
    protected log(message: string) {
      if (this.settings.clientLogActive) {
        this.messageService.add(`MashStepsService: ${message}`);
      }
    }

    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    protected handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {

        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead

        // TODO: better job of transforming error for user consumption
        this.log(`${operation} failed: ${error.message}`);

        // Let the app keep running by returning an empty result.
        return of(result as T);
      };
    }
  }
