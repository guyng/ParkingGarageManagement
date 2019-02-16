import {MatSnackBar} from '@angular/material';
import { Injectable } from '@angular/core';

@Injectable()
export class ToastService{
    /**
     *
     */
    constructor(private snackBar: MatSnackBar) {
        
    }

    public Show(message:string,action:string = null,duration:number = 1500): void {
        this.snackBar.open(message,action,{duration: duration});
    }
}