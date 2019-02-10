import { OnInit, Component } from "@angular/core";
import { getLocaleDateTimeFormat } from "@angular/common";
import { HttpClient, HttpParams } from "@angular/common/http";

@Component({
    selector: 'app-check-out-component',
    templateUrl: './check-out.component.html'
  })

export class CheckOutComponent implements OnInit  {
    public value: Date = new Date();
    public vehicleList:any[] = [];
    private bouncer : NodeJS.Timer;
    /**
     *
     */
    constructor(private http: HttpClient) {
        
    }
    ngOnInit(): void {
    //    throw new Error("Method not implemented.");
    }

    public onIdChange(id: string): void{
        var self = this;
        if (this.bouncer)
        {
            clearTimeout(this.bouncer);
        }
        this.bouncer = setTimeout(() => {
            let params = new HttpParams();
            params = params.append('personId', id);         
            this.http.get<any[]>('api/Parking/GetPersonVehicles',{params:params}).subscribe(result => {
                this.vehicleList = result;
            }, error => 
            {
                console.log(error);
            });
        }, 2000);
        console.log(id);
    }
}