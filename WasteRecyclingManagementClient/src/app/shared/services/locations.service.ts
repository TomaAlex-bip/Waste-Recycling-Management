import { TokenService } from './token.service';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, tap } from 'rxjs/operators';
import { IRecyclingPoint } from 'src/app/objects/IRecyclingPoint';


@Injectable({
    providedIn: 'root'
})
export class LocationsService {
    
    // private wasteRecyclingManagementApiUrl = 'api/recyclingPoints/recyclingPoints.json';
    private wasteRecyclingManagementApiUrl = 'https://localhost:7196/api/public/locations';

    constructor(private http: HttpClient, private tokenService: TokenService){

    }

    async getRecyclingPoints(): Promise<Observable<IRecyclingPoint[]>> {

        // get the token
        let token = await this.tokenService.getPublicToken();

        let header = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        return this.http.get<IRecyclingPoint[]>(this.wasteRecyclingManagementApiUrl, header).pipe(
            tap(data => console.log(data)),
            catchError(this.handleError)
        );
    }
    
    private handleError(err: HttpErrorResponse) {
        let errorMessage = "";
        if(err.error instanceof ErrorEvent) {
            errorMessage = `An error occurred: ${err.error.message}`;
        }
        else {
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
    }

    
}