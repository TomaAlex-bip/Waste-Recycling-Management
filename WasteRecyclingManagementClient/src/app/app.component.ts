import { IRecyclingPoint } from './objects/IRecyclingPoint';
import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { LogoutService } from './shared/services/logout.service';
import { LocationService } from './shared/services/location.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  drawerOpened = false;
  isLogged = false; 

  username = "";
  role = "";
  selectedRecyclingPoint: IRecyclingPoint | undefined;

  constructor(private logoutService: LogoutService, private locationService: LocationService, private snackBar: MatSnackBar){
    let sessionUsername = sessionStorage.getItem('username');
    let sessionRole = sessionStorage.getItem('role');
    if (sessionUsername != null && sessionRole != null) {
      this.username = sessionUsername;
      this.role = sessionRole;
      this.isLogged = true;
    }
  }

  toggleDrawer(): void {
    this.drawerOpened = !this.drawerOpened;
  }

  async openDrawer(arg: IRecyclingPoint) {
    this.drawerOpened = true;
    this.selectedRecyclingPoint = arg;

    this.selectedRecyclingPoint = await this.locationService.getRecyclingPoint(this.selectedRecyclingPoint.id);
    console.log(this.selectedRecyclingPoint);

    this.changeRecyclingPoint(this.selectedRecyclingPoint);
  }

  openLoginForm(): void {
    this.drawerOpened = true;
  }

  onLoginSuccess(username: string) {
    this.drawerOpened = false;
    this.isLogged = true;
    this.username = username;
    let role = sessionStorage.getItem('role');
    if(role != null) {
      this.role = role;
    }
  }

  onLogoutUser() {
    this.isLogged = false;
    this.username = "";
    this.role = "";
    this.drawerOpened = true;
    this.logoutService.logoutUser();
    this.snackBar.open("Logged out successfully!", "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] }  );
  }

  closeDrawer() {
    this.selectedRecyclingPoint = undefined;
    this.drawerOpened = false;
  }

  changeRecyclingPointSubject: Subject<IRecyclingPoint> = new Subject<IRecyclingPoint>();
  changeRecyclingPoint(recyclingPoint: IRecyclingPoint) {
    this.changeRecyclingPointSubject.next(recyclingPoint);
  }

}
