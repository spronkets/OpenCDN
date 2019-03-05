import { Component, OnInit } from '@angular/core';

import { LoadingComponent } from '../../shared';
import { PingControllerService } from './services/ping-controller.service';

@Component({
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit, LoadingComponent {
    pongData: string;

    constructor(private pingController: PingControllerService) {
    }

    ngOnInit(): void {
        this.pingController.ping.subscribe((pong: { data: string }) => {
            console.log('ping', pong);
            if (pong && pong.data) {
                this.pongData = pong.data;
            }
        });
    }

    get loading(): boolean {
        return !this.pongData;
    }
}
