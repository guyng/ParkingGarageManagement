import { VehicleDimensionData } from "./VehicleDimensionData.model";
import { PersonData } from "./PersonData.model";

export class CheckInData {
  constructor() {
    this.personData = new PersonData();
    this.vehicleDimensionData = new VehicleDimensionData();
  }
  public personData: PersonData;
  public vehicleDimensionData: VehicleDimensionData;
};
