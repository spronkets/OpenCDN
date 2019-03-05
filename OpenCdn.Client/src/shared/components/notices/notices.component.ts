import { Component, Input, Output, EventEmitter } from '@angular/core';

import { NoticeType } from './notice-type';
import { NoticesService } from './notices.service';

@Component({
    selector: 'notices',
    templateUrl: './notices.component.html',
    styleUrls: ['./notices.component.scss'],
    providers: [NoticesService]
})
export class NoticesComponent {
    constructor(private noticesService: NoticesService) {
    }

    private get notices() {
        return this.noticesService.notices;
    }

    private removeNotice(noticeIndex: number) {
        this.noticesService.removeNotice(noticeIndex);
    }
}
