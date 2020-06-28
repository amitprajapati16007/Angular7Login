// import { Injectable } from "@angular/core";
// import { Observable } from "rxjs";
// import { ApiRes } from "../models/apires.model";
// import { BaseApiService } from "./baseapiservice";
// import { LoginModel } from "../models/account/login.model";
// import { RegisterModel } from "../models/account/register.model";
// import { SetPasswordModel } from "../models/account/setpassword.model";
// import { ResetPasswordModel } from "../models/account/resetpassword.model";
// import { ChangePasswordModel } from "../models/account/changepassword.model";
// import { HttpClient } from "@angular/common/http";

// @Injectable()
// export class AccountService extends BaseApiService {
//     constructor(_httpClient: HttpClient) {
//         super(_httpClient);
//     }

//     public login(username: string, password: string): Observable<ApiRes> {
//         let model = new LoginModel();
//         model.userName = username;
//         model.password = password;
//         model.rememberMe = false;

//         return this.postWithoutAuth("/api/account/login", model);
//     }

//     public register(model: RegisterModel): Observable<ApiRes> {
//         return this.postWithoutAuth("/api/account/register", model);
//     }

//     public confirmEmail(email: string, code: string): Observable<ApiRes> {
//         let params = { email: email, code: code };
//         return this.getByParamsWithoutAuth("/api/account/confirmemail", params);
//     }

//     public setPassword(model: SetPasswordModel): Observable<ApiRes> {
//         return this.postWithoutAuth("/api/account/setpassword", model);
//     }

//     public checkUserNameExists(username: string): Observable<ApiRes> {
//         return this.getByParamsWithoutAuth("/api/account/check/usernameexist", { userName: username });
//     }

//     public requestResetPassword(email: string): Observable<ApiRes> {
//         return this.getByParamsWithoutAuth("/api/account/forgotpassword", { email: email });
//     }

//     public resetPassword(model: ResetPasswordModel): Observable<ApiRes> {
//         return this.postWithoutAuth("/api/account/resetpassword", model);
//     }

//     public changePassword(model: ChangePasswordModel): Observable<ApiRes> {
//         return this.post("/api/account/changepassword", model);
//     }

//     public logout(): Observable<ApiRes> {
//         return this.get("/api/account/logout");
//     }
// }
