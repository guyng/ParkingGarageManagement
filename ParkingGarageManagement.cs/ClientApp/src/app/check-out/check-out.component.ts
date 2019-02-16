import { OnInit, Component } from "@angular/core";
import { getLocaleDateTimeFormat } from "@angular/common";
import { HttpClient, HttpParams } from "@angular/common/http";
import { ToastService } from "src/shared/Services/toast.service";

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
    constructor(private http: HttpClient,private toast: ToastService) {
        
    }
    ngOnInit(): void {
    //    throw new Error("Method not implemented.");
    }

    public onIdChange(id: string): void{
        if (!id)
        {
            return;
        }
        var self = this;
        this.loadingVehicleList = true;
        if (this.bouncer)
        {
            clearTimeout(this.bouncer);
        }
        this.bouncer = setTimeout(() => {
            let params = new HttpParams();
            params = params.append('personTz', id);         
            this.http.get<any[]>('api/Parking',{params:params}).subscribe(result => {
                debugger;
                this.vehicleList = result;
                this.loadingVehicleList = false;
                if (result && result.length > 0)
                {
                    this.toast.Show(`${result.length} Vehicles have been found`);
                }
                else{
                    this.toast.Show('No Vehicles have been found');
                }
            }, error => 
            {
                console.log(error);
                this.loadingVehicleList = false;
                this.toast.Show('No Vehicles have been found');
            });
        }, 2000);
        console.log(id);
    }
}