import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { UserService } from '../services/user.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router, private userService: UserService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const userToken = this.userService.CurrentUserToken();
        if (userToken != null) {
            req = req.clone({
                setHeaders: {
                    Authorization: "Bearer " + userToken
                }
            });

            return next.handle(req).pipe(
                tap(
                    success => { },
                    err => {
                        if (err.status == 401) {
                            localStorage.removeItem('token');
                            this.router.navigateByUrl('/Login');
                        }
                    }));
        }

        else {
            return next.handle(req.clone());
        }
    }
}