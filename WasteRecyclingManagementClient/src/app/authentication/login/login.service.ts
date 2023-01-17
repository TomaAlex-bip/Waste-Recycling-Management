import { Injectable } from "@angular/core";
import { TokenService } from 'src/app/shared/services/token.service';

@Injectable({
    providedIn: 'root'
})
export class LoginService {

    private userTokenUrl: string = "https://localhost:9999/connect/token";

    constructor(private tokenService: TokenService){
        
    }

    async loginUser(username: string, password: string, role: string): Promise<boolean> {

        let scope = "wasteRecyclingApi.users";
        if(role == 'employees') { 
            scope = "wasteRecyclingApi.employees wasteRecyclingApi.users";
        }
        else if (role == 'admin') {
            scope = 'wasteRecyclingApi.admin wasteRecyclingApi.users';
        }

        // get the token
        let token = await this.tokenService.getPrivateToken(username, password, scope);

        if(token != undefined && token != null) {
            // save access_token to session storage
            sessionStorage.setItem('token', token);
            sessionStorage.setItem('username', username);
            sessionStorage.setItem('role', role);
            return true;
        }
        else {
            return false;
        }
    }

}