import { Component } from '@angular/core';
import { NgbModalConfig, NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-confirm',
  templateUrl: './modal.confirm.component.html'
})

export class ModalConfirmComponent {
  header:string = 'Mensagem';
  message:string ='';
  onClickConfirm: Function = () => {};

  constructor(config: NgbModalConfig, public modalService: NgbModal, public activeModal: NgbActiveModal) {
    config.backdrop = 'static';
    config.keyboard = false;
  }

  open(content: any, _message: string) {
    const modalRef = this.modalService.open(content);
    modalRef.componentInstance.message = _message;
    return modalRef;
  }

  close(){
    this.modalService.dismissAll();
  }

  setConfirmButton( _confirm: Function){
    this.onClickConfirm = () => {
      _confirm();
      this.activeModal.close();
    };
  }
}
