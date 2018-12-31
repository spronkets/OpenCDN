import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UploadFile} from 'ngx-file-drop';

import { FileInfo } from '../../../shared';

@Component({
    selector: 'upload-part',
    templateUrl: './upload-part.component.html',
    styleUrls: ['./upload-part.component.scss']
})
export class UploadPartComponent {
    @Input() file: UploadFile;
    @Output() onSubmitInfo: EventEmitter<FileInfo> = new EventEmitter();

    private fileInfo: FileInfo = <FileInfo>{};
    private submitted: boolean = false;
    
    private submit() {
        // TODO: re-open for changes (submit multiple times)
        if (!this.submitted && this.fileInfo.id && this.fileInfo.type && this.fileInfo.name && this.fileInfo.description) {
            this.onSubmitInfo.next(this.fileInfo);
            this.submitted = true;
        }
    }
}
