import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class LogoutService {

    constructor(){
        
    }

    logoutUser(): void {
        // delete token from session
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('username');
        sessionStorage.removeItem('role');
        console.log("delogat cu success!");
    }

}