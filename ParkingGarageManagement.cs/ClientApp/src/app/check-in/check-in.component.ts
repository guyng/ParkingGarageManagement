import { Component, OnInit } from '@angular/core';
import {CheckInData} from "../../shared/models/CheckInData.model";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-check-in-component',
  templateUrl: './check-in.component.html'
})
export class CheckInComponent implements OnInit  {
  private checkInData: CheckInData = new CheckInData();
  public checkInForm: FormGroup;

  constructor(private httpClient: HttpClient) {  }
  ngOnInit(): void {
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
