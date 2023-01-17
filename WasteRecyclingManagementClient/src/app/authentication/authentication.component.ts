import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  loginForm: boolean = true;

  registeredUsername: string | undefined;

  @Output() onLoginSuccess: EventEmitter<string> = new EventEmitter<string>();

  constructor(private snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  toggleAuth(event: boolean) {
    this.loginForm = !this.loginForm;
  }

  loginSuccess(username: string) {
    this.onLoginSuccess.emit(username);
    this.snackBar.open("Logged in as: " + username, "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] } );
  }

  registerSuccess(username: string) {
    this.loginForm = true;
    this.registeredUsername = username;
    this.snackBar.open("Successfully registered a new account with username: " + username, "Ok", { panelClass: ['mat-toolbar', 'mat-primary'] }  );
  }

}
