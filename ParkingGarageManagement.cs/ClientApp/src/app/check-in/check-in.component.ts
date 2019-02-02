import { Component, OnInit } from '@angular/core';
import {CheckInData,VehicleType, TicketType} from "../../shared/models/CheckInData.model";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-check-in-component',
  templateUrl: './check-in.component.html'
})

export class CheckInComponent implements OnInit  {
  private checkInData: CheckInData = new CheckInData();
  public checkInForm: FormGroup;
  public vehicleTypesMap: Array<{text:string,value:number}> = [];
  public ticketTypesMap: Array<{text:string,value:number}> = [];
  public selectedVehicleType: {text:string,value:number};
  public selectedTicketType: {text:string,value:number};
  constructor(private httpClient: HttpClient) {  }

  //TODO: Move this method to a utils section
  private convertEnumToObj(enumToConvert) : Array<{text:string,value:number}>
  {
    var resultObject = new Array<{text:string,value:number}>();
    for (var item in enumToConvert) {
      let itemIndex = parseInt(item);
      if (!isNaN(itemIndex))
      {
        resultObject.push({text: enumToConvert[itemIndex], value : itemIndex});
      }
    }
    return resultObject;
  }

  ngOnInit(): void {
    this.vehicleTypesMap = this.convertEnumToObj(VehicleType);
    this.ticketTypesMap = this.convertEnumToObj(TicketType);
    this.selectedVehicleType = this.vehicleTypesMap[0];
    this.selectedTicketType = this.ticketTypesMap[0];
    this.checkInForm = new FormGroup({
      'name': new FormControl(this.checkInData.personData.name, [
        Validators.required,
        Validators.minLength(4)
      ]),
      'personId': new FormControl(this.checkInData.personData.personId, [Validators.required,
        Validators.minLength(6)]),
      'phone': new FormControl(this.checkInData.personData.phone, [Validators.required,
        Validators.minLength(6)]),
      'height': new FormControl(this.checkInData.vehicleDimensionData.height, [Validators.required]),
      'width': new FormControl(this.checkInData.vehicleDimensionData.width, [Validators.required]),
      'length': new FormControl(this.checkInData.vehicleDimensionData.length, [Validators.required])
    }, { updateOn: 'blur' });
  }

  public onSubmit(): void {
    debugger;
    this.checkInData.vehicleType = this.selectedVehicleType.value as VehicleType;
    this.checkInData.ticketType = this.selectedTicketType.value as TicketType;
    debugger;
    this.httpClient.post("api/Parking", this.checkInData).subscribe(result => {
 //       this.errors = [];
        debugger;
 //       this.successfulSave = true;
        setTimeout(_ => {
 //         this.router.navigate(['login']);
        }, 1000);
      },
      error => {
 //       this.errors = [];
        let validationErrorDictionary = error.error;
        for (var fieldName in validationErrorDictionary) {
          if (validationErrorDictionary.hasOwnProperty(fieldName)) {
 //           this.errors.push(validationErrorDictionary[fieldName]);
          }
        }
      });
  }
}
