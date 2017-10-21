import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Donation } from "./donation.model";
import { Observable } from "rxjs/Observable";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class DonationsService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public addOrUpdate(options: { donation: Donation, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/donations/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ donations: Array<Donation> }> {
        return this._httpClient
            .get<{ donations: Array<Donation> }>(`${this._baseUrl}/api/donations/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<{ donation:Donation}> {
        return this._httpClient
            .get<{donation: Donation}>(`${this._baseUrl}/api/donations/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { donation: Donation, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/donations/remove?id=${options.donation.id}&correlationId=${options.correlationId}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
