import { Injectable } from '@angular/core';

import { NoticeType } from './notice-type';
import { Notice } from './notice';

@Injectable()
export class NoticesService {
    notices: Notice[] = [];

    addNotice(noticeType: NoticeType, noticeText: string) {
        const notice: Notice = {
            type: noticeType,
            text: noticeText
        };
        this.notices.push(notice);
    }

    removeNotice(noticeIndex: number) {
        if (noticeIndex >= 0 && this.notices.length >= noticeIndex) {
            this.notices.splice(noticeIndex, 1);
        }
    }
}
