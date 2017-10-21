import {Component, ChangeDetectorRef, NgZone} from "@angular/core";
import {DonationsService} from "./donations.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./donation-paginated-list-page.component.html",
    styleUrls: ["./donation-paginated-list-page.component.css"],
    selector: "ce-donation-paginated-list-page"   
})
export class DonationPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _donationsService: DonationsService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router,
        private _ngZone: NgZone
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {                  
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Donations] DonationAddedOrUpdated") {
                this._ngZone.run(() => {
                    this._donationsService.get().toPromise().then(x => {
                        this.unfilteredDonations = x.donations;
                        this.donations = this.filterTerm != null ? this.filteredDonations : this.unfilteredDonations;
                    });
                });                    
            } else if (x.type == "[Donations] DonationAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredDonations = (await this._donationsService.get().toPromise()).donations;   
        this.donations = this.filterTerm != null ? this.filteredDonations : this.unfilteredDonations;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredDonations = pluckOut({
            items: this.unfilteredDonations,
            value: $event.detail.donation.id
        });

        this.donations = this.filterTerm != null ? this.filteredDonations : this.unfilteredDonations;
        
        this._donationsService.remove({ donation: $event.detail.donation, correlationId }).subscribe();
    }

    public tryToEdit($event) {
        this._router.navigate(["donations", $event.detail.donation.id]);
    }

    public handleDonationsFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.donations = this.filterTerm != null ? this.filteredDonations : this.unfilteredDonations;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _donations: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public donations: Array<any> = [];
    public unfilteredDonations: Array<any> = [];
    public get filteredDonations() {
        return this.unfilteredDonations.filter((x) => x.name.toLowerCase().indexOf(this.filterTerm.toLowerCase()) > -1);
    }
}
