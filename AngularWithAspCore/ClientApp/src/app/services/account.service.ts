import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRes } from "../models/api-res.model";
import { BaseApiService } from "../services/base-api-service.service";
import { RegisterModel } from "../models/account/register.model";
import { LoginModel } from "../models/account/login-model";
import { HttpClient } from "@angular/common/http";
import { Component, Inject } from '@angular/core';
@Injectable()
export class AccountService extends BaseApiService {
    url: string;
    constructor(_httpClient: HttpClient,@Inject('BASE_URL') baseUrl: string) {
        super(_httpClient);
        this.url=baseUrl;
    }

    public login(username: string, password: string): Observable<ApiRes> {
         let model = new LoginModel();
        model.userName = username;
        model.password = password;
        model.rememberMe = false;

        return this.postWithoutAuth("/api/ApplicationUser/LoginAsync", model);
    }

    public register(model: RegisterModel): Observable<ApiRes> {
        return this.postWithoutAuth('/api/ApplicationUser/PostApplicationUser', model);
     }
    // public confirmEmail(email: string, code: string): Observable<ApiRes> {
    //     let params = { email: email, code: code };
    //     return this.getByParamsWithoutAuth("/api/account/confirmemail", params);
    // }

    // public setPassword(model: SetPasswordModel): Observable<ApiRes> {
    //     return this.postWithoutAuth("/api/account/setpassword", model);
    // }

    // public checkUserNameExists(username: string): Observable<ApiRes> {
    //     return this.getByParamsWithoutAuth("/api/account/check/usernameexist", { userName: username });
    // }

    // public requestResetPassword(email: string): Observable<ApiRes> {
    //     return this.getByParamsWithoutAuth("/api/account/forgotpassword", { email: email });
    // }

    // public resetPassword(model: ResetPasswordModel): Observable<ApiRes> {
    //     return this.postWithoutAuth("/api/account/resetpassword", model);
    // }

    // public changePassword(model: ChangePasswordModel): Observable<ApiRes> {
    //     return this.post("/api/account/changepassword", model);
    // }

    // public logout(): Observable<ApiRes> {
    //     return this.get("/api/account/logout");
    // }
}
