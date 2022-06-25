import { Component, OnInit } from '@angular/core';
import { timer } from 'rxjs';
import { Batch } from 'src/models/Batch';
import { CommonService } from '../common.service';

@Component({
  selector: 'app-current-status',
  templateUrl: './current-status.component.html'
})
export class CurrentStatusComponent implements OnInit {

  data: Batch[] = [];
  pollingInterval: number = 5000;
  constructor(private commonService: CommonService) { }

  ngOnInit(): void {
    this.poll();
  }

  private poll() {
    const source = timer(0, this.pollingInterval);
    const subscribe = source.subscribe(val => this.sendRequest());
  }

  private sendRequest() {
    this.commonService.fetchCurrentStatus().subscribe(data => {
      this.data = data;
    });
  }

}
