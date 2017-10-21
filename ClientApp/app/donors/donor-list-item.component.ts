import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./donor-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./donor-list-item.component.css"
    ],
    selector: "ce-donor-list-item"
})
export class DonorListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();		
    }
      
    @Input()
    public donor: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
