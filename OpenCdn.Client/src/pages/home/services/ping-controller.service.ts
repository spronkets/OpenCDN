import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class PingControllerService {
    private pingUrl: string = 'http://localhost:5000/api/ping';  // URL to web api

    constructor(private http: HttpClient) {
    }

    get ping(): Observable<{ data: string }> {
        return this.http.get<{ data: string }>(this.pingUrl);
    }
}
