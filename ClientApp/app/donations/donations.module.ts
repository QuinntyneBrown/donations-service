import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../shared/shared.module";
import { UsersModule } from "../users/users.module";

import { AuthGuardService } from "../shared/guards/auth-guard.service";
import { TenantGuardService } from "../shared/guards/tenant-guard.service";
import { EventHubConnectionGuardService } from "../shared/guards/event-hub-connection-guard.service";
import { CurrentUserGuardService } from "../users/current-user-guard.service";

import { DonationsService } from "./donations.service";

import { DonationEditComponent } from "./donation-edit.component";
import { DonationEditPageComponent } from "./donation-edit-page.component";
import { DonationListItemComponent } from "./donation-list-item.component";
import { DonationPaginatedListComponent } from "./donation-paginated-list.component";
import { DonationPaginatedListPageComponent } from "./donation-paginated-list-page.component";

export const DONATION_ROUTES: Routes = [{
    path: 'donations',
    component: DonationPaginatedListPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'donations/create',
    component: DonationEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'donations/:id',
    component: DonationEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
}];

const declarables = [
    DonationEditComponent,
    DonationEditPageComponent,
    DonationListItemComponent,
    DonationPaginatedListComponent,
    DonationPaginatedListPageComponent
];

const providers = [DonationsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, RouterModule.forChild(DONATION_ROUTES), SharedModule, UsersModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class DonationsModule { }
