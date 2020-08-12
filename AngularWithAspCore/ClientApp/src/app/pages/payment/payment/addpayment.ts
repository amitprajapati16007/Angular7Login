import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ngbd-modal-content',
  templateUrl: './addpayment.html'
})
export class AddPayment {
  @Input() name;

  constructor(public activeModal: NgbActiveModal) {}
}
