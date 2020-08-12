import { Component, OnInit, OnDestroy } from "@angular/core";
import { AccountService } from "../services/account.service";
import { AuthServiceSys } from "../services/auth-service.service";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { AuthService } from "angularx-social-login";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers: [AccountService],
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isExpanded = false;
  
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  fullname = "";
  isUserLoggedIn: boolean = false;
  
  private currUserSetSubscription: Subscription;
  private currUserRemovedSubscription: Subscription;

  constructor(
      private AuthServiceSys: AuthServiceSys,
      private accountService: AccountService,
      private router: Router,
      private authService: AuthService,
  ) {
  }

  ngOnInit(): void {
      this.isUserLoggedIn = this.AuthServiceSys.isUserLoggedIn();
      debugger;
      if (this.isUserLoggedIn) {
          let currUser = this.AuthServiceSys.getCurrentUser();
          if (currUser != null)
              this.fullname = currUser.firstName + " " + currUser.lastName;

      }

      this.currUserSetSubscription = this.AuthServiceSys.onCurrUserSet.subscribe(currUser => {
          if (currUser != null) {
              this.fullname = currUser.firstName + " " + currUser.lastName;
              this.isUserLoggedIn = true;
          }
      });

      this.currUserRemovedSubscription = this.AuthServiceSys.onCurrUserRemoved.subscribe(isRemoved => {
          if (isRemoved) {
              this.fullname = "";
              this.isUserLoggedIn = false;
          }
      });

      
  }

  ngOnDestroy(): void {
      this.currUserSetSubscription.unsubscribe();
      this.currUserRemovedSubscription.unsubscribe();
  }

  onLogout() {
      this.accountService.logout().subscribe(res => {
          if (res.status === 1) {
              this.AuthServiceSys.removeCurrentUser();
              // remove from social login 
              debugger;
              this.authService.signOut();
              this.router.navigate(['/login']);
          }
      });
  }
}

