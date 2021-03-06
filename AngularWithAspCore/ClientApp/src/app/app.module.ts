import { BrowserModule } from '@angular/platform-browser';
import { NgModule,ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule, AuthGuardService } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './pages/account/login/login.component';
import { RegisterComponent } from './pages/account/register/register.component';
import { ToastrModule } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmEmailComponent } from './pages/account/confirmemail/confirmemail.component';
import { ForgotpasswordComponent } from './pages/account/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './pages/account/resetpassword/resetpassword.component';
import { AuthServiceSys } from './services/auth-service.service';
import { ChangePasswordComponent } from './pages/account/change-password/change-password.component';
import { RequestInterceptor } from './misc/request-interceptor';
import { ResponseInterceptor } from './misc/response-interceptor';
import { GlobalErrorHandler } from './misc/global-error-handler';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PaymentComponent } from './pages/payment/payment/payment.component';
import { AppCommonModule } from './app-common-module';
import { SocialLoginModule, AuthServiceConfig } from "angularx-social-login";
import {  FacebookLoginProvider,GoogleLoginProvider } from "angularx-social-login";
import { AddPayment } from 'src/app/pages/payment/payment/addpayment';




let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("36217918798-ilrukrjjqov0kovue5atcifajqjtmv2j.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("906223719882190")
  }
]);

export function provideConfig() {
  return config;
}
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    ConfirmEmailComponent,
    ForgotpasswordComponent,
    ResetpasswordComponent,
    ChangePasswordComponent,
    PaymentComponent,
    AddPayment
  ],
  entryComponents: [AddPayment],
  imports: [
    AppCommonModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(AppRoutingModule),
    CommonModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    NgbModule,
    SocialLoginModule
  ],
  providers: [
    AuthServiceSys,
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ResponseInterceptor,
      multi: true,
    },
    {
        provide: ErrorHandler,
        useClass: GlobalErrorHandler
    },
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
