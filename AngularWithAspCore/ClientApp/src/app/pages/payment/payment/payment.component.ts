import { Component, OnInit } from '@angular/core';
import { Query, Sort, Paginator } from '../../../misc/query';
import { PaymentViewModel } from '../../payment/payment-view-model';
import { BaseApiService } from 'src/app/services/base-api-service.service';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddPayment } from 'src/app/pages/payment/payment/addpayment';
import { fail } from 'assert';
@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  providers: [BaseApiService,NgbModalConfig, NgbModal]
})
export class PaymentComponent implements OnInit {
  sort: Sort = new Sort();
  paginator: Paginator = new Paginator();
  data: Array<PaymentViewModel> = [];
  loading = false;
  filter: any = {
    cardOwnerName: null,
    cardNumber: null,
    expirationDate: null
  };
  
  constructor(private apiService: BaseApiService,config: NgbModalConfig, private modalService: NgbModal) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
  }
 
  addedit(obj) {
    const modalRef = this.modalService.open(AddPayment);
    if(obj){
      modalRef.componentInstance.isedit = true;
      modalRef.componentInstance.payment=obj;
    }    
    else {
      modalRef.componentInstance.isedit = false;
      modalRef.componentInstance.payment=new PaymentViewModel();
    } 
    
    
    modalRef.componentInstance.childEvent.subscribe(($e) => {
      this.getPayments();
    })
  }
  ngOnInit() {
  }
  getPayments() {
    let q: Query = new Query(this.paginator.pageNo, this.paginator.pageSize);
    q.addSort(this.sort);

    // if (this.filter.cardOwnerName)
    //     q.addCondition("ProductName", this.filter.name.trim(), Operator.StartsWith);
    // if (this.filter.pricegt && isNumber(Number(this.filter.pricegt)))
    //     q.addCondition("UnitPrice", Number(this.filter.pricegt.trim()), Operator.Ge);
    // if (this.filter.pricelt && isNumber(Number(this.filter.pricelt)))
    //     q.addCondition("UnitPrice", Number(this.filter.pricelt.trim()), Operator.Le);
    // if (this.filter.category) 
    //     q.addCondition("categoryID", Number(this.filter.category.trim()));

    this.loading = true;
    this.apiService.getByParams("api/PaymentDetail/getpaymentlist", { q: q }).subscribe(result => {
        this.paginator.totalItems = result.data.total;
        this.data = <Array<PaymentViewModel>>result.data.data;
        this.loading = false;
    });
}
}
