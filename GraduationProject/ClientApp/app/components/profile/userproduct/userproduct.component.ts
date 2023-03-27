import { UserService } from './../../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from './../../../services/product.service';
import { ProductInfo } from './../../../models/productCard';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-userproduct',
  templateUrl: './userproduct.component.html',
  styleUrls: ['./userproduct.component.css']
})
export class UserproductComponent implements OnInit {
  ProductsInfo: ProductInfo[] = null;
  constructor(public ProductService: ProductService
    , private activeRouter: ActivatedRoute,
    public UserService:UserService ) { }

  ngOnInit(): void {
    this.activeRouter.params.subscribe(a => {
      if (a.id == null) {
        this.ProductService.GetUserProduct().subscribe(a => {
          this.ProductsInfo = a;
         
        });
      }
      else
      {
        this.ProductService.GetUserProductById(a.id).subscribe(a => {
          this.ProductsInfo = a;
        });
      }

    });
   
  }

  Example(data) {
    alert(data);
  }


}
