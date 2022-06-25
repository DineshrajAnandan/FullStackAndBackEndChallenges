import { Component, Input, OnInit } from '@angular/core';
import { Batch } from 'src/models/Batch';

@Component({
  selector: 'app-status-view',
  templateUrl: './status-view.component.html'
})
export class StatusViewComponent implements OnInit {

  @Input() data: Batch[] = [];
  constructor() { }

  ngOnInit(): void {
  }

}
