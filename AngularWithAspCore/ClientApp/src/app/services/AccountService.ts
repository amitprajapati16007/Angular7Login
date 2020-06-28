import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRes } from "../models/api-res.model";
import { BaseApiService } from "../services/base-api-service.service";
import { RegisterModel } from "../models/account/register.model";
import { HttpClient } from "@angular/common/http";
@Injectable()
export class AccountService extends BaseApiService {
    constructor(_httpClient: HttpClient) {
        super(_httpClient);
    }
    // public login(username: string, password: string): Observable<ApiRes> {
    //     let model = new LoginModel();
    //     model.userName = username;
    //     model.password = password;
    //     model.rememberMe = false;
    //     return this.postWithoutAuth("/api/account/login", model);
    // }
    public register(model: RegisterModel): Observable<ApiRes> {
        return this.postWithoutAuth("/api/ApplicationUser/PostApplicationUser", model);
    }
}
