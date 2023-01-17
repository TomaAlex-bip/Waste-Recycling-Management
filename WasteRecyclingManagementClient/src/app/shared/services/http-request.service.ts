import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class HttpRequestService {

    constructor(private http: HttpClient){
        
    }

    async makeRequest(body: any, header: any, url: string): Promise<any> {

        return await new Promise<any>( resolve => {
            this.http.post(url, body, header)
            .subscribe({
                
                next: (data) => {
                    let response = JSON.parse(JSON.stringify(data));
                    resolve(response);
                },

                error: (error) => {
                    let response = JSON.parse(JSON.stringify(error));
                    resolve(response);
                }
            });
        });
    }

    async makeDeleteRequest(header: any, url: string): Promise<any> {

        return await new Promise<any>( resolve => {
            this.http.delete(url, header)
            .subscribe({
                
                next: (data) => {
                    let response = JSON.parse(JSON.stringify(data));
                    resolve(response);
                },

                error: (error) => {
                    let response = JSON.parse(JSON.stringify(error));
                    resolve(response);
                }
            });
        });
    }

}