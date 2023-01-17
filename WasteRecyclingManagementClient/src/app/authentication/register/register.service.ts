import { HttpRequestService } from '../../shared/services/http-request.service';
import { User } from '../../objects/User';
import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { TokenService } from 'src/app/shared/services/token.service';

@Injectable({
    providedIn: 'root'
})
export class RegisterService {

    private registerUrl: string = "https://localhost:7196/api/public/registration";

    constructor(private tokenService: TokenService, private httpRequestService: HttpRequestService){
        
    }

    async registerUser(username: string, password: string): Promise<string> {
        
        // get the token
        let token = await this.tokenService.getPublicToken();
        
        // call the api to register
        let header = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }
        let registrationResponse: any = await this.httpRequestService.makeRequest(new User(username, password), header, this.registerUrl);
        if(registrationResponse.error == null) {
            return "success";
        }
        else {
            return registrationResponse.error.errorMessage.errorMessage;
        }
    }

}