import { OnInit, Component } from "@angular/core";
import { getLocaleDateTimeFormat } from "@angular/common";
import { HttpClient, HttpParams } from "@angular/common/http";

@Component({
    selector: 'app-check-out-component',
    templateUrl: './check-out.component.html'
  })

export class CheckOutComponent implements OnInit  {
    public checkoutDate: Date = new Date();
    public checkoutHour: Date = new Date();
    public vehicleList:any[] = [];
    private bouncer : NodeJS.Timer;
    public loadingVehicleList:Boolean = false;
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
        this.loadingVehicleList = true;
        if (this.bouncer)
        {
            clearTimeout(this.bouncer);
        }
        this.bouncer = setTimeout(() => {
            let params = new HttpParams();
            params = params.append('personId', id);         
            this.http.get<any[]>('api/Parking',{params:params}).subscribe(result => {
                debugger;
                this.vehicleList = result;
                this.loadingVehicleList = false;
            }, error => 
            {
                console.log(error);
            });
        }, 2000);
        console.log(id);
    }
}