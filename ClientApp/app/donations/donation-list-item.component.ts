import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./donation-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./donation-list-item.component.css"
    ],
    selector: "ce-donation-list-item"
})
export class DonationListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();		
    }
      
    @Input()
    public donation: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
