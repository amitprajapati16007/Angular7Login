import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from "../../../services/AccountService";
import { RegisterModel } from '../../../models/account/register.model';
//import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
//import { pageSlideUpAnimation } from '../../misc/page.animation';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [AccountService],
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
    loading = false;
    submitted = false;

    
    constructor(
        private accountService: AccountService,
        private formBuilder: FormBuilder,
//        private toastrService: ToastrService,
        private router: Router) {
    }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            FirstName: ['', Validators.required],
            LastName: ['', Validators.required],
            Email: ['', Validators.required],
            Password: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }
    onRegister() {
        this.loading = true;

        if (this.registerForm.invalid) {
            this.loading = false;
            return;
        }

        let model = new RegisterModel();
        model.Email = this.registerForm.value.Email;
        model.FirstName = this.registerForm.value.FirstName;
        model.LastName = this.registerForm.value.LastName;
        model.Password = this.registerForm.value.Password;

        // model = new RegisterModel();

        this.accountService.register(model).subscribe(
            res => {
                switch (res.status) {
                    case 1:
                      //  this.isConfirmEmailSent = true;
                        this.registerForm.reset();
                        break;
                    case -3:
                        //this.toastrService.error("The email you have entered, is already exists. Please try another email.");
                        break;
                    case -4:
                        //this.toastrService.error("User was successfully created but we failed to sent email. Please try again.");
                        break;
                    case -6:
                        //this.toastrService.error("User is successfully created but we failed to sent email. Please contact administrator for help.");
                        break;
                    case -7:
                        //this.toastrService.error("User is successfully created but we failed to set role. Please contact administrator for help.");
                        break;
                    case -8:
                        //this.toastrService.error("User was successfully created but we failed to set role. Please try again.");
                        break;
                    default:
                        //this.toastrService.error("Some error occured on server during registration process. Please try again.");
                        break;
                }

                this.loading = false;
            },
            err => {
                this.loading = false;
            });
    }

}
