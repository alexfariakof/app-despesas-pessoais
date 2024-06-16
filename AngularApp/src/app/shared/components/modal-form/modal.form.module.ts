import { NgModule } from '@angular/core';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalFormComponent } from '..';

@NgModule({
  declarations: [ModalFormComponent],
  providers: [NgbModalConfig, NgbModal],
})

export class ModalFormModule {}
