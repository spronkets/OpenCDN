import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppHeaderComponent } from './app-header/app-header.component';
import { AppMainComponent } from './app-main/app-main.component';
import { AppComponent } from './app.component';

@NgModule({
    declarations: [
        AppHeaderComponent,
        AppMainComponent,
        AppComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
