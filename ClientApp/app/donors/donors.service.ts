import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Donor } from "./donor.model";
import { Observable } from "rxjs/Observable";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class DonorsService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public addOrUpdate(options: { donor: Donor, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/donors/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ donors: Array<Donor> }> {
        return this._httpClient
            .get<{ donors: Array<Donor> }>(`${this._baseUrl}/api/donors/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<{ donor:Donor}> {
        return this._httpClient
            .get<{donor: Donor}>(`${this._baseUrl}/api/donors/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { donor: Donor, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/donors/remove?id=${options.donor.id}&correlationId=${options.correlationId}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
