import {Component} from "@angular/core";
import {DonationsService} from "./donations.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./donation-edit-page.component.html",
    styleUrls: ["./donation-edit-page.component.css"],
    selector: "ce-donation-edit-page"
})
export class DonationEditPageComponent {
    constructor(private _donationsService: DonationsService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.donation = (await this._donationsService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).donation;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._donationsService.addOrUpdate({ donation: $event.detail.donation, correlationId }).subscribe();
        this._router.navigateByUrl("/donations");
    }

    public donation = {};
}
