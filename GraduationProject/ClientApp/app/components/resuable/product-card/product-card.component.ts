import { WishlistService } from './../../../services/wishlist.service';
import { ProductInfo } from './../../../models/productCard';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() productInfo: ProductInfo = new ProductInfo();
  @Input() clickable: boolean = false;
  @Input() Link = "";
  @Input() IsLike = false;
  @Input() ShowLike = false;
  @Output() Onlike = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
   
  }
  FlagLike() {
 
    this.Onlike.emit(this.IsLike);
    this.IsLike = !this.IsLike;
   
  }

}
