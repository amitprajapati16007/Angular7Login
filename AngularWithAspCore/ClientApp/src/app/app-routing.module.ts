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
//import { AuthService } from "./services/app.auth.service";

// @NgModule({
//   imports: [
//     CommonModule
//   ],
//   declarations: []
// })

//import { AuthService } from "./services/app.auth.service";


// @Injectable()
// export class AuthGuardService implements CanActivate {
//     constructor( private router: Router) { }

//     canActivate() {
//      // this.router.navigate(["/login"]);
//       return false;
//     }
// }


export const AppRoutingModule : Routes = [ 
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: "confirmemail", component: ConfirmEmailComponent },
]