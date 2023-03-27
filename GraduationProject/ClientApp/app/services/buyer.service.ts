import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Buyer } from '../models/buyer';
import { ProductIsSoldViewModel } from '../models/productSold';
import { BuyersReview } from '../models/BuyersReview';

@Injectable({
  providedIn: 'root'
})
export class BuyerService {

  constructor(private http:HttpClient) {

  }
  AddBuyer(productId: number) {
    return this.http.post("/api/Buyer/" + productId, null);
  }
  SelectBuyers(productId: number) {
    return this.http.get<Buyer[]>("/api/Buyer/" + productId);
  }
  IsOwnerOfProduct(productId: number) {
    return this.http.get<boolean>("/api/Buyer/IsOwner/" + productId);
  }
  ProductSold(product:ProductIsSoldViewModel) {
    return this.http.put("/api/Buyer/", product);
  }
  GetUserProductToReview() {
    return this.http.get<BuyersReview[]>("/api/Buyer/GetMyReviews");

  }

  

}
