import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class OperationService {

    private userOperationUri = 'https://localhost:7196/api/users/operation';
    private employeeOperationUri = "https://localhost:7196/api/employees/cleaning";

    constructor(private http: HttpClient){

    }

    async makeOperation(recyclingPointName: string, containerWasteType: string, wasteAmount: number): Promise<string> {
        
        let operationUri = "";
        let operationBody: any;
        let role = sessionStorage.getItem('role');
        if(role != null) {
            if (role == 'users') {
                operationBody = this.makeUserOperationBody(recyclingPointName, containerWasteType, wasteAmount);
                operationUri = this.userOperationUri;
            }
            else if (role == 'employees') {
                operationBody = this.makeEmployeeOperationBody(recyclingPointName, containerWasteType);
                operationUri = this.employeeOperationUri;
            }
        }

        if(operationBody == null) {
            return "Bad input!";
        }

        let token = sessionStorage.getItem('token');
        let header = {
            headers: new HttpHeaders({'Authorization': 'Bearer ' + token})
        }

        return await new Promise<string>( resolve => {
            this.http.post(operationUri, operationBody, header)
            .subscribe({
                next: (data) => {
                    let res = JSON.parse(JSON.stringify(data));
                    if(res.errorMessage == null) {
                        resolve('success');
                    }
                    else {
                        resolve('Unexpected error!');
                    }
                },

                error: (error) => {
                    let res = JSON.parse(JSON.stringify(error));
                    // console.log(res);
                    if(res.error != null) {
                        resolve(res.error.errorMessage.errorMessage);
                    }
                    else {
                        resolve('Unexpected error!');
                    }
                }
            });
        });
    }
    
    makeUserOperationBody(recyclingPointName: string, containerWasteType: string, wasteAmount: number): any {
        if(recyclingPointName.length <= 5 || containerWasteType.length <= 1 || wasteAmount < 0.1) {
            return null;
        }

        let operationBody = {
            recyclingPointName: recyclingPointName,
            containerWasteType: containerWasteType,
            wasteAmount: wasteAmount
        }

        return operationBody;
    }

    makeEmployeeOperationBody(recyclingPointName: string, containerWasteType: string): any {
        if(recyclingPointName.length <= 5 || containerWasteType.length <= 1) {
            return null;
        }

        let operationBody = {
            recyclingPointName: recyclingPointName,
            containerWasteType: containerWasteType
        }

        return operationBody;
    }

}