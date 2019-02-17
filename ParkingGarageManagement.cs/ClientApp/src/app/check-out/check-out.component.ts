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
    private chosenVehicleId:string;
    private chosenPersonId:string;
    public vehicleList:any[] = [];
    public personTzList:any[] = [];
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

    // public onIdChange(id: string): void{
    //     if (!id)
    //     {
    //         return;
    //     }
    //     var self = this;
    //     this.loadingVehicleList = true;
    //     if (this.bouncer)
    //     {
    //         clearTimeout(this.bouncer);
    //     }
    //     this.bouncer = setTimeout(() => {
    //         // let params = new HttpParams();
    //         // params = params.append('personTz', id);
            
    //         // this.http.get<any[]>('api/Parking/GetPersonIds',{params:params}).subscribe(result => {
    //         //     this.personTzList = result;
    //         // });

    //         this.http.get<any[]>('api/Parking',{params:params}).subscribe(result => {
    //             debugger;
    //             this.vehicleList = result;
    //             this.loadingVehicleList = false;
    //             if (result && result.length > 0)
    //             {
    //                 this.toast.Show(`${result.length} Vehicles have been found`);
    //             }
    //             else{
    //                 this.toast.Show('No Vehicles have been found');
    //             }
    //         }, error => 
    //         {
    //             console.log(error);
    //             this.loadingVehicleList = false;
    //             this.toast.Show('No Vehicles have been found');
    //         });
    //     }, 2000);
    //     console.log(id);
    // }

    public fetchPeople(value) : void{
        debugger;
        let params = new HttpParams();
        params = params.append('inputPersonTz', value);
        
        this.http.get<any[]>('api/Parking/GetPersonIds',{params:params}).subscribe(result => {
            this.personTzList = result;
        });

    }

    public selectVehicle(value):void {
        debugger;
        this.chosenVehicleId = value.vehicleId;
    }

    public selectPerson(value):void{
        let params = new HttpParams();
        this.chosenPersonId = value;
        params = params.append('personTz', value);
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
    }

    public CheckOut():void{

        this.http.post('api/Parking',{VehicleId: this.chosenVehicleId, PersonId: this.chosenPersonId}).subscribe(result => {
    });
}
}