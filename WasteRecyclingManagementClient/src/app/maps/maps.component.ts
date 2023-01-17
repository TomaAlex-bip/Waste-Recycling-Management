import { MatSnackBar } from '@angular/material/snack-bar';
import { AdminService } from './../menu/admin.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IRecyclingPoint } from '../objects/IRecyclingPoint'
import { LocationsService } from '../shared/services/locations.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-maps',
  templateUrl: './maps.component.html',
  styleUrls: ['./maps.component.css']
})
export class MapsComponent implements OnInit {
  centerLatitude = 47.159561347663065;
  centerLongitude = 27.587569748980947;
  zoom = 14;
  markerUrl = "assets\\images\\point.png";

  recyclingPoints: IRecyclingPoint[] = []

  newRecyclingPointName = "";
  newRecyclingPointLatitude = -1.0;
  newRecyclingPointLongitude = -1.0;
  isNewRecyclingPointOn = false;
  newRecyclingPointError: boolean = false;
  newRecyclingPointErrorMessage: string = "";

  @Input() role = "users";

  @Output() locationClick: EventEmitter<IRecyclingPoint> = new EventEmitter<IRecyclingPoint>();

  @Output() onRecyclingPointCardClose: EventEmitter<void> = new EventEmitter<void>();

  constructor(private locationService: LocationsService, private adminService: AdminService, private snackBar: MatSnackBar) { }

  async ngOnInit() {
    await this.loadLocations();
    this.eventsSubscription = this.recyclingPointSearched?.subscribe((data) => this.centerOnLocation(data));
  }

  private eventsSubscription: Subscription | undefined;
  @Input() recyclingPointSearched: Observable<IRecyclingPoint> | undefined;

  async loadLocations() {
    let response = await this.locationService.getRecyclingPoints()
    response.subscribe({
      next: locations => {
        this.recyclingPoints = locations;
        // console.log(locations);
      },
      error: err => console.error(err)
    });
  }

  onClickOnMap(event: any) {
    this.isNewRecyclingPointOn = true;
    this.newRecyclingPointLatitude = event.coords.lat;
    this.newRecyclingPointLongitude = event.coords.lng;
  }
  
  async onRecyclingPointAdd() {
    
    let status = await this.adminService.addRecyclingPoint(this.newRecyclingPointName, this.newRecyclingPointLatitude, this.newRecyclingPointLongitude);

    if(status == 'success') {
      this.snackBar.open("Successfully created a new recycling point with name: " + this.newRecyclingPointName, "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] }  );
      await this.loadLocations();
      this.onRecyclingPointCancel();
    }
    else {
      this.newRecyclingPointError = true;
      this.newRecyclingPointErrorMessage = status;
    }
  }

  onRecyclingPointCancel() {
    this.newRecyclingPointError = false;
    this.newRecyclingPointErrorMessage = "";
    this.newRecyclingPointLatitude = 0;
    this.newRecyclingPointLongitude = 0;
    this.newRecyclingPointName = "";
    this.isNewRecyclingPointOn = false;
  }

  onLocationClick(event: IRecyclingPoint) {
    // console.log(event);
    this.centerLatitude = event.latitude;
    this.centerLongitude = event.longitude
    this.zoom = 18;
    this.locationClick.emit(event);
  }

  onRecyclingPointClose() { 
    this.onRecyclingPointCardClose.emit();
  }

  centerOnLocation(recyclingPoint: IRecyclingPoint) {
    this.centerLatitude = recyclingPoint.latitude;
    this.centerLongitude = recyclingPoint.longitude;
    this.zoom = 18;
  }

  changeMapZoom(e: any) {
    this.zoom = e;
  }

}
