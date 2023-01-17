import { RegisterService } from './register.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  hide = true;

  private _username: string = "";
  private _password: string = "";
  private _passwordRepeat: string = "";

  errorMessage = "error"
  usernameWarning = false;
  passwordWarning = false;
  passwordRepeatWarning = false;
  serverError = false;

  constructor(private registerService: RegisterService) { }

  @Output() authSwitchClick: EventEmitter<boolean> = new EventEmitter<boolean>();

  @Output() onRegisterSuccess: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {
  }

  openLoginForm(): void
  {
    this.authSwitchClick.emit(true);
  }

  onUsernameChange(event: any) {
    this._username = event.target.value;
  }

  onPasswordChange(event: any) { 
    this._password = event.target.value;
  }

  onPasswordRepeatChange(event: any) { 
    this._passwordRepeat = event.target.value;
  }

  async onRegisterUser() {

    if(this._username.includes(" ") || this._username.length < 4) {
      this.usernameWarning = true;
      this.passwordWarning = false;
      this.passwordRepeatWarning = false;
    }
    else if(this._password.length < 4) {
      this.passwordWarning = true;
      this.usernameWarning = false;
      this.passwordRepeatWarning = false;
    }
    else if(this._password != this._passwordRepeat) {
      this.passwordRepeatWarning = true;
      this.usernameWarning = false;
      this.passwordWarning = false;
    }
    else {
      let status = await this.registerService.registerUser(this._username, this._password);

      if(status == 'success') {
        this.usernameWarning = false;
        this.passwordWarning = false;
        this.passwordRepeatWarning = false;
        
        this.onRegisterSuccess.emit(this._username);
      }
      else {
        this.errorMessage = status;
        this.serverError = true;
      }
    }

  }

}
