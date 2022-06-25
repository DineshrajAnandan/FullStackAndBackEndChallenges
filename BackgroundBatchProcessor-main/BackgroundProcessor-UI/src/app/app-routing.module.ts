import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CurrentStatusComponent } from './current-status/current-status.component';
import { HomeComponent } from './home/home.component';
import { PreviousStatusComponent } from './previous-status/previous-status.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'currentStat', component: CurrentStatusComponent },
  { path: 'previousStat', component: PreviousStatusComponent },
  { path: 'home', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
