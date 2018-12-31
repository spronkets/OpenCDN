import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { FileDropModule } from 'ngx-file-drop';

import {
    BrowseComponent,
    UploadPartComponent,
    UploadComponent
} from './pages';

@NgModule({
    declarations: [
        AppComponent,
        BrowseComponent,
        UploadPartComponent,
        UploadComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        FileDropModule,
        RouterModule.forRoot([
            { path: '', component: BrowseComponent, pathMatch: 'full' },
            { path: 'browse', component: BrowseComponent },
            { path: 'upload', component: UploadComponent }
        ])
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
