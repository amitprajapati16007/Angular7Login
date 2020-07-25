import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { AuthServiceSys } from '../../../services/auth-service.service'
import { AuthService } from "angularx-social-login";
import { FacebookLoginProvider } from "angularx-social-login";
import { SocialUser } from "angularx-social-login";

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    providers: [AccountService]
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;

    private user: SocialUser;
    private loggedIn: boolean;

    constructor(
        private accountService: AccountService,
        private formBuilder: FormBuilder,
        private toastrService: ToastrService,
        private router: Router,
        private authServicesys: AuthServiceSys,
        private authService: AuthService,
    ) {}

    ngOnInit(): void {
        this.loginForm = this.formBuilder.group({
            userName: ["", [Validators.required]],
            password: ["", [Validators.required, Validators.minLength(8)]]
        });

        this.authService.authState.subscribe((user) => {
            debugger;
            this.user = user;
            this.loggedIn = (user != null);
          });
    }
    signInWithFB(): void {        
        this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(x => console.log(x));
      } 

      signOut(): void {
        this.authService.signOut();
      }
     
    get f() {
        return this.loginForm.controls;
    }

    onLogin() {
        this.loading = true;

        if (this.loginForm.invalid) {
            this.loading = false;
            return;
        }

        let username = this.loginForm.value.userName;
        let password = this.loginForm.value.password;

        this.accountService.login(username, password).subscribe(
            res => {
                debugger;
                switch (res.status) {
                    case 1:
                        this.authServicesys.setCurrentUser(res.data);
                        this.router.navigate(["/home"]);
                         break;
                     default:
                         this.toastrService.error(res.message);
                         break;
                 }
                this.loading = false;
            },
            err => {
                this.loading = false;
            }
        );
    }
}
