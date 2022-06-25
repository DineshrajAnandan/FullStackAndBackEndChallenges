import { Component, OnInit } from '@angular/core';
import { Batch } from 'src/models/Batch';
import { CommonService } from '../common.service';

@Component({
  selector: 'app-previous-status',
  templateUrl: './previous-status.component.html',
  //styleUrls: ['./previous-status.component.scss']
})
export class PreviousStatusComponent implements OnInit {

  data: Batch[] = [];
  constructor(private commonService: CommonService) { }

  ngOnInit(): void {
    this.sendRequest();
  }

  private sendRequest() {
    this.commonService.fetchPreviousStatus().subscribe(data => {
      this.data = data;
    });
  }
}
