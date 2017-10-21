import {Component, ChangeDetectorRef, NgZone} from "@angular/core";
import {DonorsService} from "./donors.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./donor-paginated-list-page.component.html",
    styleUrls: ["./donor-paginated-list-page.component.css"],
    selector: "ce-donor-paginated-list-page"   
})
export class DonorPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _donorsService: DonorsService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router,
        private _ngZone: NgZone
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {                  
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Donors] DonorAddedOrUpdated") {
                this._ngZone.run(() => {
                    this._donorsService.get().toPromise().then(x => {
                        this.unfilteredDonors = x.donors;
                        this.donors = this.filterTerm != null ? this.filteredDonors : this.unfilteredDonors;                        
                    });
                }
            } else if (x.type == "[Donors] DonorAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredDonors = (await this._donorsService.get().toPromise()).donors;   
        this.donors = this.filterTerm != null ? this.filteredDonors : this.unfilteredDonors;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredDonors = pluckOut({
            items: this.unfilteredDonors,
            value: $event.detail.donor.id
        });

        this.donors = this.filterTerm != null ? this.filteredDonors : this.unfilteredDonors;
        
        this._donorsService.remove({ donor: $event.detail.donor, correlationId }).subscribe();
    }

    public tryToEdit($event) {
        this._router.navigate(["donors", $event.detail.donor.id]);
    }

    public handleDonorsFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.donors = this.filterTerm != null ? this.filteredDonors : this.unfilteredDonors;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _donors: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public donors: Array<any> = [];
    public unfilteredDonors: Array<any> = [];
    public get filteredDonors() {
        return this.unfilteredDonors.filter((x) => x.name.toLowerCase().indexOf(this.filterTerm.toLowerCase()) > -1);
    }
}
