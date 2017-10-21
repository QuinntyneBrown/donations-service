import {Component} from "@angular/core";
import {DonorsService} from "./donors.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./donor-edit-page.component.html",
    styleUrls: ["./donor-edit-page.component.css"],
    selector: "ce-donor-edit-page"
})
export class DonorEditPageComponent {
    constructor(private _donorsService: DonorsService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.donor = (await this._donorsService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).donor;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._donorsService.addOrUpdate({ donor: $event.detail.donor, correlationId }).subscribe();
        this._router.navigateByUrl("/donors");
    }

    public donor = {};
}
