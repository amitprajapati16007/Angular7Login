import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRes } from "../models/api-res.model";
import { BaseApiService } from "../services/base-api-service.service";
import { RegisterModel } from "../models/account/register.model";
import { LoginModel } from "../models/account/login-model";
import { ResetPasswordModel } from "../models/account/reset-password-model";
import { ChangePasswordModel } from "../models/account/change-password-model";
import { HttpClient } from "@angular/common/http";
import { Component, Inject } from '@angular/core';
import { SocialUser } from "angularx-social-login";
import { PaymentViewModel } from '../pages/payment/payment-view-model';
@Injectable()
export class PaymentService extends BaseApiService {
    url: string;
    constructor(_httpClient: HttpClient,@Inject('BASE_URL') baseUrl: string) {
        super(_httpClient);
        this.url=baseUrl;
    }

    public PostpaymentDetail(model: PaymentViewModel): Observable<ApiRes> {
        return this.post("/api/PaymentDetail/postpaymentdetail", model);
    }

}
