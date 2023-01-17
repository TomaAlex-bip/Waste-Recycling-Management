import { MatSnackBar } from '@angular/material/snack-bar';
import { AdminService } from './admin.service';
import { OperationService } from './operation.service';
import { IRecyclingPoint } from '../objects/IRecyclingPoint';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { LocationService } from '../shared/services/location.service';

interface WasteType {
  type: string;
  unit: string;
  free: number;
  occupied: string;
}

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  wasteTypeControl = new FormControl<WasteType | null>(null, Validators.required);
  selectFormControl = new FormControl('', Validators.required);
  wasteTypes: WasteType[] = [];

  selectedWasteType: any;
  selectedAmount = 0;

  operationError = false;
  operationErrorMessage = "";

  newContainerType = "";
  newContainerMeasureUnit = "";
  newContainerCapacity = 0;

  @Input() role = "users";

  @Input() isLogged: boolean = false;

  @Input() recyclingPoint: IRecyclingPoint | undefined;

  private eventsSubscription: Subscription | undefined;
  @Input() recyclingPointChange: Observable<IRecyclingPoint> | undefined;

  @Output() onOperationSuccess: EventEmitter<void> = new EventEmitter<void>();

  constructor(private locationService: LocationService, private operationService: OperationService, private adminService: AdminService, private snackBar: MatSnackBar) { 
    
  }
  
  ngOnInit(): void {
    this.eventsSubscription = this.recyclingPointChange?.subscribe((data) => this.updateRecyclingPoint(data));
  }

  updateRecyclingPoint(recyclingPoint: IRecyclingPoint) {

    this.recyclingPoint = recyclingPoint;

    // console.log("update recycling point");
    // console.log(this.recyclingPoint);

    this.wasteTypes = [];
    if(this.recyclingPoint?.containers != undefined) {
      for (let container of this.recyclingPoint.containers) {
        let occupied = container.occupied + " / " + container.totalCapacity;
        this.wasteTypes.push( {type: container.type, unit: container.measureUnit, free: (container.totalCapacity - container.occupied), occupied: occupied} );
      }
    }
  }

  async makeOperation() {
    if(this.recyclingPoint != undefined && this.selectedWasteType != undefined) {
      let status = await this.operationService.makeOperation(this.recyclingPoint?.name, this.selectedWasteType.type, this.selectedAmount);

      // console.log("operation result!!!!!");
      // console.log(status);

      if(status == 'success') {
        this.snackBar.open("Operation done successfully at " + this.recyclingPoint.name + " for waste type: " + this.selectedWasteType.type, "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] }  );

        this.operationError = false;
        this.operationErrorMessage = "";
        this.selectedAmount = 0;
        this.selectedWasteType = undefined;
        this.onOperationSuccess.emit();
        this.recyclingPoint = undefined;
      }
      else {
        this.operationError = true;
        this.operationErrorMessage = status;
      }
    }
    else {
      this.operationError = true;
      this.operationErrorMessage = "Fill input first!";
    }
  }
  
  async addContainer() {
    console.log("new container:");
    console.log(this.recyclingPoint?.id);
    console.log(this.newContainerType);
    console.log(this.newContainerMeasureUnit);
    console.log(this.newContainerCapacity);
    if(this.recyclingPoint != null) {
      let status = await this.adminService.addContainer(this.recyclingPoint?.id, this.newContainerType, this.newContainerMeasureUnit, this.newContainerCapacity);
      if(status == 'success') {
        this.snackBar.open("Successfully created a new container of type " + this.newContainerType + " at " + this.recyclingPoint.name, "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] }  );
        this.operationError = false;
        this.operationErrorMessage = "";
        this.recyclingPoint = undefined;
        this.onOperationSuccess.emit();
      }
      else {
        this.operationError = true;
        this.operationErrorMessage = status;
      }
    }
    else {
      this.operationError = true;
      this.operationErrorMessage = "Unexpected error!";
    }
  }

  async deleteRecyclingPoint() {
    console.log(this.recyclingPoint?.name);
    if(this.recyclingPoint != null) {
      let status = await this.adminService.removeRecyclingPoint(this.recyclingPoint.id);
      if(status == 'success') {
        this.snackBar.open("Successfully deleted " + this.recyclingPoint.name, "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] }  );
        this.operationError = false;
        this.operationErrorMessage = "";
        this.recyclingPoint = undefined;
        this.onOperationSuccess.emit();
      }
      else {
        this.operationError = true;
        this.operationErrorMessage = status;
      }
    }
    else {
      this.operationError = true;
      this.operationErrorMessage = "Unexpected error!";
    }
  }
  


}
