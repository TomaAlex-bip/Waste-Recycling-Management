import { LoginService } from './login.service';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private _username: string = "";
  set username(value: string) {
    this._username = value;
  }
  private _password: string = "";
  set password(value: string) {
    this._password = value;
  }

  passwordVisibility = false;
  wrongCredentialsWarn = false;

  selectedRole = "users";
  
  constructor(private loginService: LoginService) { }
  
  @Input() registeredUsername: string | undefined;
  
  @Output() authSwitchClick: EventEmitter<boolean> = new EventEmitter<boolean>();

  @Output() loginSuccess: EventEmitter<string> = new EventEmitter<string>();

  @ViewChild("username", { static: true })
  usernameInput!: ElementRef;

  ngOnInit(): void {
    if(this.registeredUsername != undefined) {
      this.usernameInput.nativeElement.value = this.registeredUsername;
      this._username = this.registeredUsername;
    }
  }

  openRegisterForm(): void
  {
    this.authSwitchClick.emit(true);
  }

  onUsernameChange(event: any) {
    this._username = event.target.value;
  }

  onPasswordChange(event: any) { 
    this._password = event.target.value;
  }

  async onLoginForm() {
    let status = await this.loginService.loginUser(this._username, this._password, this.selectedRole);

    if (status) {
      this.wrongCredentialsWarn = false
      this.loginSuccess.emit(this._username);
    }
    else {
      this.wrongCredentialsWarn = true;
    }
  }

}
