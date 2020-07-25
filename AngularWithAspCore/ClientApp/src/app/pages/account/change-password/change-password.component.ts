import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../../services/account.service';
import { AuthServiceSys } from '../../../services/auth-service.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ChangePasswordModel } from '../../../models/account/change-password-model';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',  
  providers: [AccountService]
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup;
  loading = false;

  constructor(
      private accountService: AccountService,
      private authService: AuthServiceSys,
      private formBuilder: FormBuilder,
      private toastrService: ToastrService,
      private router: Router
  ) {}

  ngOnInit(): void {
      this.changePasswordForm = this.formBuilder.group({
          newPassword: ["", [Validators.required, Validators.minLength(8)]],
          currentPassword: ["", [Validators.required]]
      });
  }

  get f() {
      return this.changePasswordForm.controls;
  }

  onChangePassword() {
      this.loading = true;

      if (this.changePasswordForm.invalid) {
          this.loading = false;
          return;
      }

      let model = new ChangePasswordModel();
      model.currentPassword = this.changePasswordForm.value.currentPassword;
      model.newPassword = this.changePasswordForm.value.newPassword;

      this.accountService.changePassword(model).subscribe(
          res => {
              if (res.status === 1) {
                  this.authService.setCurrentUser(res.data);
                  this.toastrService.success(res.message);
                  this.router.navigate(["/home"]);
                  this.changePasswordForm.reset();
              } else if (res.status === 2) {
                this.toastrService.error(res.message);
              }
              this.loading = false;
          },
          err => {
              this.loading = false;
          }
      );
  }

}
