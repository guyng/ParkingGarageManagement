import { Component, OnInit } from '@angular/core';
import { CheckInData, VehicleType, TicketType } from "../../shared/models/CheckInData.model";
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-check-in-component',
  templateUrl: './check-in.component.html'
})

export class CheckInComponent implements OnInit {
  private checkInData: CheckInData = new CheckInData();
  public checkInForm: FormGroup;
  public vehicleTypesMap: Array<{ text: string, value: number }> = [];
  public ticketTypesMap: Array<{ text: string, value: number }> = [];
  public errors: string[] = [];
  public selectedVehicleType: { text: string, value: number };
  public selectedTicketType: { text: string, value: number };
  public checkinSuccess: boolean = false;
  constructor(private httpClient: HttpClient,
    private formBuilder: FormBuilder, private router: Router) {

  }

  //TODO: Move this method to a utils section
  private convertEnumToObj(enumToConvert): Array<{ text: string, value: number }> {
    var resultObject = new Array<{ text: string, value: number }>();
    for (var item in enumToConvert) {
      let itemIndex = parseInt(item);
      if (!isNaN(itemIndex)) {
        resultObject.push({ text: enumToConvert[itemIndex], value: itemIndex });
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
      name: new FormControl(this.checkInData.personData.name, [
        Validators.required,
        Validators.minLength(4)
      ]),
      personId: new FormControl(this.checkInData.personData.personId, [Validators.required,
      Validators.pattern("^[0-9]*$"),
      Validators.minLength(6)]),
      phone: new FormControl(this.checkInData.personData.phone, [Validators.required,
      Validators.pattern("^[0-9]*$"),
      Validators.minLength(6)]),
      height: new FormControl(this.checkInData.vehicleDimensionData.height,
        [Validators.required, Validators.minLength(6)]),

      width: new FormControl(this.checkInData.vehicleDimensionData.width, [Validators.required,
      Validators.pattern("^[0-9]*$")]),
      length: new FormControl(this.checkInData.vehicleDimensionData.length, [Validators.required,
      Validators.pattern("^[0-9]*$")])
    });
  }

  get name() { return this.checkInForm.get('name'); }


  public onSubmit(): void {
    debugger;
    this.checkInData.vehicleType = this.selectedVehicleType.value as VehicleType;
    this.checkInData.ticketType = this.selectedTicketType.value as TicketType;
    debugger;
    this.httpClient.post("api/Parking", this.checkInData).subscribe((result: any) => {
      debugger;
      this.errors = [];
      if (result === 202) {
        this.checkinSuccess = true;
        setTimeout(() => {
          this.router.navigate(['fetch-data']);
        }, 2000);
      }
      else {
        let chosenTicket = result.chosenTicket;
        this.errors.push(`You have chosen ${chosenTicket.name} ticket. Allowed height: ${chosenTicket.maxHeight}, Width:${chosenTicket.maxWidth},Length: ${chosenTicket.maxLength}`);
        this.errors.push("The inserted width,height or length did not meet the requirements.");
        this.errors.push("Either correct your input or choose a different ticket type of the following:");
        for (let ticket of result.alternativeTickets) {
          let alternativeTicketMessage = `Ticket: ${ticket.name}. Price difference: ${ticket.cost - chosenTicket.cost}`;
          debugger;
          this.errors.push(alternativeTicketMessage);
          console.log(ticket);
        }
      }

    },
      error => {
        debugger;
        this.errors = [];
        let validationErrorDictionary = error.error;
        for (var fieldName in validationErrorDictionary) {
          if (validationErrorDictionary.hasOwnProperty(fieldName)) {
            this.errors.push(validationErrorDictionary[fieldName]);
          }
        }
      });
  }
}
