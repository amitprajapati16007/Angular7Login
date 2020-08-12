import { Component, Input,Output,EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ngbd-modal-content',
  templateUrl: './addpayment.html'
})
export class AddPayment {
  @Input() name;
  @Output() childEvent = new EventEmitter();
  test(){
    this.activeModal.dismiss('Cross click');
    this.childEvent.emit();
}
  constructor(public activeModal: NgbActiveModal) {}
}
