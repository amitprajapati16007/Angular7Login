import { Component, Input,Output,EventEmitter ,OnInit} from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PaymentViewModel } from '../../payment/payment-view-model';
import { PaymentService } from '../../../services/payment.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'ngbd-modal-content',
  templateUrl: './addpayment.html',
  providers: [PaymentService]
})
export class AddPayment implements OnInit  {
  @Input() isedit;
  @Input() payment=new PaymentViewModel();
  @Output() childEvent = new EventEmitter();
  payemntForm: FormGroup;
  loading = false;
  constructor(public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private paymentService: PaymentService,
    private toastrService: ToastrService,) {}

  ngOnInit() {
    // this.payemntForm = this.formBuilder.group({
    //     CardOwnerName: [this.payment.cardOwnerName, Validators.required],
    //     CardNumber: [this.payment.cardNumber, [Validators.required, Validators.minLength(16)]],
    //     expirationDate: [this.payment.expirationDate, [Validators.required, Validators.minLength(5)]],
    //     CVV: [this.payment.cVV, [Validators.required, Validators.minLength(3)]]
    // });

    this.payemntForm = this.formBuilder.group({
        CardOwnerName: [this.payment.cardOwnerName, Validators.required],
        CardNumber: [this.payment.cardNumber, [Validators.required, Validators.minLength(16)]],
        expirationDate: [this.payment.expirationDate, [Validators.required, Validators.minLength(5)]],
        CVV: [this.payment.cvv, [Validators.required, Validators.minLength(3)]]
    });

}
get f() { return this.payemntForm.controls; }
onSave() {
    this.loading = true;

    if (this.payemntForm.invalid) {
        this.loading = false;
        return;
    }

    let model = new PaymentViewModel();
    if(this.isedit){
        

    }  
    else{
        model.pmid = 0;
    }
    debugger;
    model.pmid = this.payment.pmid;
    model.cardOwnerName = this.payemntForm.value.CardOwnerName;
    model.cardNumber = this.payemntForm.value.CardNumber;
    model.expirationDate = this.payemntForm.value.expirationDate;
    model.cvv = this.payemntForm.value.CVV;

    // model = new RegisterModel();

    this.paymentService.PostpaymentDetail(model).subscribe(
        res => {
            switch (res.status) {
                case 1:
                    this.toastrService.success(res.message);
                    this.payemntForm.reset();
                    this.activeModal.dismiss('Cross click');
                    this.childEvent.emit();
                    break;
                default:
                    this.toastrService.error(res.message);
                    break;
            }

            this.loading = false;
        },
        err => {
            this.loading = false;
        });
}

}
