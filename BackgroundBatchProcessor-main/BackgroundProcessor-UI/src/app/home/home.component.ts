import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { CommonService } from '../common.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {

  requestInProgress: boolean = false;
  batchesNos: number = 0;
  nosPerBatch: number = 0;

  inputOptions = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

  constructor(private commonService: CommonService,
    private toastr: ToastrService) {

  }

  startProcess() {
    if (this.batchesNos == 0 || this.nosPerBatch == 0)
    {
      this.toastr.error("Please choose numbers");
      return;
    }
    this.requestInProgress =  true;
    this.commonService.startProcess(this.batchesNos, this.nosPerBatch)
      .pipe(take(1))
      .subscribe(res => {
        this.toastr.success(res);
        this.requestInProgress = false;
      },err => {
        this.toastr.error(err.error);
        this.requestInProgress = false;
      });
  }

}
