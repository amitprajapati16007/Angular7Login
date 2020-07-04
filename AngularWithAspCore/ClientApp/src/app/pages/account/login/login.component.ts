import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/app.auth.service';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../../services/AccountService';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
    private accountService: AccountService,
      private formBuilder: FormBuilder,
      private authService: AuthService,
      private toastrService: ToastrService,
  ) {
      // // redirect to home if already logged in
      // if (this.authenticationService.currentUserValue) { 
      //     this.router.navigate(['/']);
      // }
  }

  ngOnInit() {
      this.loginForm = this.formBuilder.group({
          username: ['', Validators.required],
          password: ['', Validators.required]
      });

      // // get return url from route parameters or default to '/'
      // this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

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
            if (res.status === 1) {
                this.authService.setCurrentUser(res.data);
                this.router.navigate(["/page1"]);
            } else if (res.status === 0) {
                this.toastrService.error("Invalid username or password!!");
            }
            this.loading = false;
        },
        err => {
            this.loading = false;
        }
    );
}
}
