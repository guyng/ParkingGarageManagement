import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-garage-state',
  templateUrl: './garage-state.component.html'
})
export class GarageStateComponent {
  public forecasts: WeatherForecast[];
  public listOfLateParkingPeople:any[] = [];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string,private datePipe: DatePipe) {
    // http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
    //   this.forecasts = result;
    // }, error => console.error(error));

    let params = new HttpParams();
    let todayDate = datePipe.transform(new Date().toString(),"MM/dd/yyyy HH:mm:ss");
    debugger;
    params = params.append('inputDate',todayDate);
    http.get<any[]>(baseUrl + 'api/Parking/GetListOfLateParkingPeople',{params: params}).subscribe(result => {
      debugger;
      this.listOfLateParkingPeople = result;
    });
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
