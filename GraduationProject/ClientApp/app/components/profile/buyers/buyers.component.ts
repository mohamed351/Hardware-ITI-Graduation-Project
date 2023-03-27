import { RatingDailogComponent } from './../../resuable/rating-dailog/rating-dailog.component';
import { Buyer } from './../../../models/buyer';
import { BuyerService } from './../../../services/buyer.service';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from './../../../services/product.service';
import { ProductInfo } from './../../../models/productCard';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-buyers',
  templateUrl: './buyers.component.html',
  styleUrls: ['./buyers.component.css']
})
export class BuyersComponent implements OnInit {
  ProductInfo: ProductInfo = new ProductInfo();
  buyers: Buyer[] = [];


  constructor(private productService: ProductService,
    private buyerService:BuyerService,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(a => {
      this.productService.GetProductById(a.id).subscribe(a => {
        this.ProductInfo = a;
      });
      this.buyerService.SelectBuyers(a.id).subscribe(a => {
        this.buyers = a;
      });
    });
    

  }
  Sold(userId, UserName) {
    let dialogRef = this.dialog.open(RatingDailogComponent, { data: { userId, UserName, productId:this.ProductInfo.userProductId } });    
  }

}
