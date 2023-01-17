import { HttpRequestService } from '../shared/services/http-request.service';
import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class AdminService {

    private addRecyclingPointUri = 'https://localhost:7196/api/admin/locations';

    constructor(private httpRequestService: HttpRequestService){

    }

    async addContainer(recyclingPointId: number, newContainerWasteType: string, newContainerMeasureUnit: string, newContainerCapacity: number): Promise<string> {
        let role = sessionStorage.getItem('role');
        if(role != null) {
            if(role != 'admin') { 
                return "Not permitted!";
            }
            else {
                let addContainerBody = [{
                            wasteType: newContainerWasteType,
                            measureUnit: newContainerMeasureUnit,
                            totalCapacity: newContainerCapacity 
                        }];

                let addContainerUri = this.addRecyclingPointUri + '/' + recyclingPointId;

                let token = sessionStorage.getItem('token');
                let header = {
                    headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
                }

                let response = await this.httpRequestService.makeRequest(addContainerBody, header, addContainerUri);

                if(response.error == null) {
                    return "success";
                }
                else {
                    return response.error.errorMessage;
                }
            }
        }
        else {
            return "Unexpected error!";
        }
    }

    async addRecyclingPoint(recyclingPointName: string, latitude: number, longitude: number): Promise<string> {
        let role = sessionStorage.getItem('role');
        if(role != null) {
            if(role != 'admin') { 
                return "Not permitted!";
            }
            else {
                let addRecyclingPointBody = {
                            name: recyclingPointName,
                            latitude: latitude,
                            longitude: longitude
                        };

                let token = sessionStorage.getItem('token');
                let header = {
                    headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
                }

                let response = await this.httpRequestService.makeRequest(addRecyclingPointBody, header, this.addRecyclingPointUri);

                if(response.error == null) {
                    return "success";
                }
                else {
                    return response.error.errors["Name"];
                }
            }
        }
        else {
            return "Unexpected error!";
        }
    }

    async removeRecyclingPoint(recyclingPointId: number) {
        let role = sessionStorage.getItem('role');
        if(role != null) {
            if(role != 'admin') { 
                return "Not permitted!";
            }
            else {
                let deleteUri = this.addRecyclingPointUri + '/' + recyclingPointId;

                let token = sessionStorage.getItem('token');
                let header = {
                    headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
                }

                let response = await this.httpRequestService.makeDeleteRequest(header, deleteUri);

                console.log(response);

                if(response == null) {
                    return "success";
                }
                else {
                    console.log(response.error);
                    return response.error.errorMessage;
                }
            }
        }
        else {
            return "Unexpected error!";
        }
    }

}