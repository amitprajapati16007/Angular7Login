import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  providers: [AccountService],
})
export class ForgotpasswordComponent implements OnInit {

  forgotPassForm: FormGroup;
  loading = false;
  isResetPasswordEmailSent = false;

  constructor(
      private accountService: AccountService,
      private formBuilder: FormBuilder,
      private toastrService: ToastrService,
  ) {
  }

  ngOnInit(): void {
      this.forgotPassForm = this.formBuilder.group({
          email: ['', [Validators.required, Validators.email]],
      });
  }

  get f() { return this.forgotPassForm.controls; }

  onResetPasswordRequest() {
      this.loading = true;
debugger;
      if (this.forgotPassForm.invalid) {
          this.loading = false;
          return;
      }

      let email = this.forgotPassForm.value.email;

      this.accountService.requestResetPassword(email).subscribe(
          res => {
            debugger;
              if (res.status === 1) {
                  this.isResetPasswordEmailSent = true;
              }
              else{
                this.toastrService.error(res.message);
              }  
              this.loading = false;
          },
          err => {
              this.loading = false;
          });
  }
}