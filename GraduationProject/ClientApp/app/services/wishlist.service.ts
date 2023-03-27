import { ProductInfo } from './../models/productCard';
import { Injectable } from '@angular/core';
import { UserProduct } from '../models/user-product';
import { PathLocationStrategy } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  ProductInfo: UserProduct[] = [];
  constructor() { 
    if (localStorage.getItem("wishlist") == null)
      localStorage.setItem("wishlist", JSON.stringify(this.ProductInfo));
    else
      this.ProductInfo = JSON.parse(localStorage.getItem("wishlist"));

  }
  SetItem(info: UserProduct) {
    if (localStorage.getItem("wishlist") == null) {
      this.ProductInfo.push(info);
    }
    else
    {
      this.ProductInfo = JSON.parse(localStorage.getItem("wishlist"));
      let prod = this.ProductInfo.find(a => a.id == info.id);
      console.log(prod);
      if (prod == null)
        this.ProductInfo.push(info);
      else
      this.ProductInfo =this.ProductInfo.filter(a => a.id != info.id);

    }
    localStorage.setItem("wishlist", JSON.stringify(this.ProductInfo));
       
  }
  IsExist(info: UserProduct):boolean {
    let isExist: boolean = false;
    if (localStorage.getItem("wishlist") != null) {
      let prod = this.ProductInfo.find(a => a.id == info.id);
      if (prod != null) {
        isExist = true;
      }
    }
    return isExist;
  }

  DeleteFromWishList(info: UserProduct) {
    if (localStorage.getItem("wishlist") != null) {
      this.ProductInfo = this.ProductInfo.filter(a=>a.id != info.id);
      localStorage.setItem("wishlist", JSON.stringify(this.ProductInfo));
    }
    
  }

}
