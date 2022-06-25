import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Batch } from 'src/models/Batch';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpClient) { }

  /**
   * startProcess
   */
  public startProcess(batchesNos: number, nosPerBatch: number): Observable<any> {
    return this.http.post(`${environment.apiUrl}process/start`,{
      batchesNos,
      nosPerBatch
    },{
      responseType: 'text'
    })
  }

  /**
   * fetchPreviousStatus
   */
  public fetchPreviousStatus(): Observable<Batch[]> {
    return this.http.get<Batch[]>(`${environment.apiUrl}process/PreviousProcessStatus`);
  }

  /**
   * fetchCurrentStatus
   */
  public fetchCurrentStatus(): Observable<Batch[]> {
    return this.http.get<Batch[]>(`${environment.apiUrl}process/CurrentProcessStatus`);
  }
}
