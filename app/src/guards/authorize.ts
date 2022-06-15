import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {
  constructor() {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const authorizeUrl = 'https://accounts.spotify.com/authorize'+
    `?client_id=${environment.clientId}`+
    `&redirect_uri=${environment.redirectUri}`+
    `&response_type=code`;

    window.location.href = authorizeUrl;
    return true;
  }
}