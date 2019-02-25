import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CheckInComponent } from './check-in/check-in.component';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { CheckOutComponent } from './check-out/check-out.component';
import { IntlModule } from '@progress/kendo-angular-intl';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { MatProgressBarModule, MatProgressSpinnerModule, MatSnackBarModule } from '@angular/material';
import { DatePipe } from '@angular/common';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { ToastService } from '../shared/Services/toast.service';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CheckInComponent,
    FetchDataComponent,
    CheckOutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NotificationModule,
    FormsModule,
    ReactiveFormsModule,
    DropDownsModule,
    MatSnackBarModule,
    IntlModule,
    MatProgressSpinnerModule,
    DateInputsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'check-in', component: CheckInComponent },
      { path: 'check-out', component: CheckOutComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
    InputsModule,
    BrowserAnimationsModule
  ],
  providers: [DatePipe,ToastService],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
