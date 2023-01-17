import { LogoutService } from './logout.service'
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IRecyclingPoint } from 'src/app/objects/IRecyclingPoint';


@Injectable({
    providedIn: 'root'
})
export class LocationService {

    private recyclingPointUri = 'https://localhost:7196/api/users/locations/';

    constructor(private http: HttpClient, private logoutService: LogoutService){

    }

    async getRecyclingPoint(id: number): Promise<IRecyclingPoint> {

        let token = sessionStorage.getItem('token');

        let header = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        let recyclingPointUri = this.recyclingPointUri + id.toString();

        return await new Promise<IRecyclingPoint>( resolve => {
            this.http.get<IRecyclingPoint>(recyclingPointUri, header)
            .subscribe({
                next: (data) => {
                    resolve(data);
                },
                error: (err) => {
                    resolve(err);
                }
            });
        });
    }
    

    
}