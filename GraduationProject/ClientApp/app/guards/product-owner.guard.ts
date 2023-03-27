import { BuyerService } from './../services/buyer.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductOwnerGuard implements CanActivate {
  /**
   *
   */
  constructor(private buyerService:BuyerService) {
 

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return new Observable<boolean>(a => {
    
      this.buyerService.IsOwnerOfProduct(next.params.id).toPromise().then(c => {
        a.next(c);
      });
     

    });
    
  }
}
