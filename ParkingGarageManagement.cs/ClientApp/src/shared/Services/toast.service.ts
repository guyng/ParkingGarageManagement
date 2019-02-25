import {MatSnackBar} from '@angular/material';
import { Injectable } from '@angular/core';
import { NotificationService } from "@progress/kendo-angular-notification";

@Injectable()
export class ToastService{
    /**
     *
     */
    constructor(private snackBar: MatSnackBar,private notification:NotificationService) {
        
    }

    public Show(message:string,action:string = null,duration:number = 1000): void {
        debugger;
        this.notification.show({
            content: message,
            cssClass: 'button-notification',
            animation: { type: 'slide', duration: 400 },
            hideAfter: duration,
            position: { horizontal: 'center', vertical: 'bottom' },
            type: { style: 'success', icon: true }
        });
    //    this.snackBar.open(message,action,{duration: duration});
    }
}