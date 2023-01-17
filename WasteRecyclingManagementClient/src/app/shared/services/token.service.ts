import { HttpRequestService } from './http-request.service';
import { HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class TokenService {

    private publicTokenUrl: string = "https://localhost:9999/connect/token";

    constructor(private httpRequestService: HttpRequestService){
        
    }

    async getPublicToken(): Promise<string> {

        let params = new HttpParams({
            fromObject: {
                client_id: 'wasteRecyclingClient.public',
                client_secret: 'secret',
                grant_type: 'client_credentials',
                scope: 'wasteRecyclingApi.public'
            }
        });

        let httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }),
        };

        let tokenResponse = await this.httpRequestService.makeRequest(params, httpOptions, this.publicTokenUrl);

        return tokenResponse.access_token;
    }

    async getPrivateToken(username: string, password: string, scope: string): Promise<string> {

        let params = new HttpParams({
            fromObject: {
                client_id: 'wasteRecyclingClient',
                client_secret: 'secret',
                grant_type: 'password',
                scope: scope,
                username: username,
                password: password
            }
        });

        let httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }),
        };

        let tokenResponse = await this.httpRequestService.makeRequest(params, httpOptions, this.publicTokenUrl);

        return tokenResponse.access_token;
    }

}