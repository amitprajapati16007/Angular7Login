import { Component, OnInit } from "@angular/core";
import { AccountService } from "../../../services/account.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    providers: [AccountService]
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;

    constructor(
        private accountService: AccountService,
        private formBuilder: FormBuilder,
        private toastrService: ToastrService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.loginForm = this.formBuilder.group({
            userName: ["", [Validators.required]],
            password: ["", [Validators.required, Validators.minLength(8)]]
        });
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
                       // this.authService.setCurrentUser(res.data);
                    this.router.navigate(["/page1"]);
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
