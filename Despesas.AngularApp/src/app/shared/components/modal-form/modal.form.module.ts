import { NgModule } from '@angular/core';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalFormComponent } from 'src/app/shared/components/modal-form/modal.form.component';

@NgModule({
  declarations: [ModalFormComponent],
  providers: [NgbModalConfig, NgbModal],
})

export class ModalFormModule {}
