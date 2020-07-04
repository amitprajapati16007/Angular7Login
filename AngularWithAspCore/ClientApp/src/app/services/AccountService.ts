import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRes } from '../models/api-res.model';
import { BaseApiService } from '../services/base-api-service.service';
import { RegisterModel } from '../models/account/register.model';
import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Injectable()
export class AccountService extends BaseApiService {
    baseUrl: string;
    constructor(_httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        super(_httpClient);
        this.baseUrl = baseUrl;
    }
    // public login(username: string, password: string): Observable<ApiRes> {
    //     let model = new LoginModel();
    //     model.userName = username;
    //     model.password = password;
    //     model.rememberMe = false;
    //     return this.postWithoutAuth("/api/account/login", model);
    // }
    public register(model: RegisterModel): Observable<ApiRes> {
        return this.postWithoutAuth('/api/ApplicationUser/PostApplicationUser', model);
    }
}
