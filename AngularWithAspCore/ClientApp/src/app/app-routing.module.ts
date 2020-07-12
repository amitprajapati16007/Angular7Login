import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './pages/account/login/login.component';
import { RegisterComponent } from './pages/account/register/register.component';
import { Injectable } from "@angular/core";
import { CanActivate } from "@angular/router";
import { Routes, Router } from "@angular/router";
import { ConfirmEmailComponent } from './pages/account/confirmemail/confirmemail.component';
import { ForgotpasswordComponent } from './pages/account/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './pages/account/resetpassword/resetpassword.component';
import { ChangePasswordComponent } from './pages/account/change-password/change-password.component';
import { AuthService } from './services/auth-service.service';

@Injectable()
export class AuthGuardService implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate() {
        if (
            this.authService.isUserLoggedIn() &&
            !this.authService.isTokenExpired()
        ) {
            return true;
        } else {
            this.router.navigate(["/login"]);
            return false;
        }
    }
}


export const AppRoutingModule : Routes = [ 
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: "confirmemail", component: ConfirmEmailComponent },
      { path: "forgotpassword", component: ForgotpasswordComponent },
      { path: "resetpassword", component: ResetpasswordComponent },
      { path: "home", component: HomeComponent },

      { path: "changepassword", component: ChangePasswordComponent , canActivate: [AuthGuardService]},
      
]