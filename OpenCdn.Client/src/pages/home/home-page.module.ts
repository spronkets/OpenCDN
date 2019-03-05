import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { HomePageRoutingModule } from './home-page-routing.module';
import { HomePageComponent } from './home-page.component';

import { PingControllerService } from './services/ping-controller.service';

@NgModule({
    declarations: [
        HomePageComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        HomePageRoutingModule
    ],
    providers: [
        PingControllerService
    ]
})
export class HomePageModule {
}
