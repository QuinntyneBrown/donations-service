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

import { DonorsService } from "./donors.service";

import { DonorEditComponent } from "./donor-edit.component";
import { DonorEditPageComponent } from "./donor-edit-page.component";
import { DonorListItemComponent } from "./donor-list-item.component";
import { DonorPaginatedListComponent } from "./donor-paginated-list.component";
import { DonorPaginatedListPageComponent } from "./donor-paginated-list-page.component";

export const DONOR_ROUTES: Routes = [{
    path: 'donors',
    component: DonorPaginatedListPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'donors/create',
    component: DonorEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'donors/:id',
    component: DonorEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
}];

const declarables = [
    DonorEditComponent,
    DonorEditPageComponent,
    DonorListItemComponent,
    DonorPaginatedListComponent,
    DonorPaginatedListPageComponent
];

const providers = [DonorsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, RouterModule.forChild(DONOR_ROUTES), SharedModule, UsersModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class DonorsModule { }
