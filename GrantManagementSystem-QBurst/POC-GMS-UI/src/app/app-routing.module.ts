import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditGrantComponent } from './components/add-edit-grant/add-edit-grant.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './helpers/auth.gaurd';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'account', loadChildren: accountModule },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
