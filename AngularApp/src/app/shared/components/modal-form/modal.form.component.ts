import { Component } from '@angular/core';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-modal-form',
  template: ''
})
export class ModalFormComponent {

  constructor(public config: NgbModalConfig, public modalService: NgbModal) {
		config.backdrop = 'static';
    config.keyboard = false;
	}

	open(content: any) {
		this.modalService.open(content, { centered: true });
	}

  close(){
    this.modalService.dismissAll();
  }
}
