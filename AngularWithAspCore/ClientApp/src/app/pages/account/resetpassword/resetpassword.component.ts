import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { ResetPasswordModel } from '../../../models/account/reset-password-model';


@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  providers: [AccountService]
})
export class ResetpasswordComponent implements OnInit {

    resetpasswordForm: FormGroup;
    loading = false;
    email: string;
    code: string;

    constructor(
        private route: ActivatedRoute,
        private accountService: AccountService,
        private formBuilder: FormBuilder,
        private toastrService: ToastrService,
        private router: Router) {
        this.email = this.route.snapshot.queryParams.email;
        this.code = this.route.snapshot.queryParams.code;

        if (!this.email || !this.code) {
            this.router.navigate(['/home']);
        }
    }

    ngOnInit(): void {
        this.resetpasswordForm = this.formBuilder.group({
            newPassword: ['', [Validators.required, Validators.minLength(8)]],
        });
    }

    get f() { return this.resetpasswordForm.controls; }

    onResetPassword() {
        this.loading = true;

        if (this.resetpasswordForm.invalid) {
            this.loading = false;
            return;
        }

        let model = new ResetPasswordModel();
        model.email = this.email;
        model.code = this.code;
        model.newPassword = this.resetpasswordForm.value.newPassword;

        this.accountService.resetPassword(model).subscribe(
            res => {
                if (res.status === 1) {
                   // this.authService.setCurrentUser(res.data);
                    this.toastrService.success(res.message);
                }
                else if (res.status === 2) {
                    this.toastrService.error(res.message);
                }
                this.resetpasswordForm.reset();
                this.loading = false;
            },
            err => {
                this.loading = false;
            });
    }

}
