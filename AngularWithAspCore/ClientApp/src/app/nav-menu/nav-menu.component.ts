import { Component, OnInit, OnDestroy } from "@angular/core";
import { AccountService } from "../services/account.service";
import { AuthService } from "../services/auth-service.service";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
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
      private authService: AuthService,
      private accountService: AccountService,
      private router: Router
  ) {
  }

  ngOnInit(): void {
      debugger;
      this.isUserLoggedIn = this.authService.isUserLoggedIn();
      if (this.isUserLoggedIn) {
          let currUser = this.authService.getCurrentUser();
          if (currUser != null)
              this.fullname = currUser.userName;

      }

      this.currUserSetSubscription = this.authService.onCurrUserSet.subscribe(currUser => {
          if (currUser != null) {
              this.fullname = currUser.userName;
              this.isUserLoggedIn = true;
          }
      });

      this.currUserRemovedSubscription = this.authService.onCurrUserRemoved.subscribe(isRemoved => {
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
              this.authService.removeCurrentUser();
              this.router.navigate(['/login']);
          }
      });
  }
}

