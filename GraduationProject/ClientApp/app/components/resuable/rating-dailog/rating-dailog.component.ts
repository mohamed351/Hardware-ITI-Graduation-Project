import { Router } from '@angular/router';
import { ProductIsSoldViewModel } from './../../../models/productSold';
import { BuyerService } from './../../../services/buyer.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-rating-dailog',
  templateUrl: './rating-dailog.component.html',
  styleUrls: ['./rating-dailog.component.css']
})
export class RatingDailogComponent implements OnInit {

  
  constructor(public dialogRef: MatDialogRef<RatingDailogComponent>,
  @Inject(MAT_DIALOG_DATA) public data: any , private BuyerService:BuyerService,private router:Router) {

  }  
  review: number;
  description: string;
  DefaultElement(element:number) {
    this.review = element;

  }

  
  ngOnInit(): void {
    
  }
  Close() {
    this.dialogRef.close();
  }
  ReviewUser() {
    let product: ProductIsSoldViewModel = {
      userID: this.data.userId,
      rating: this.review,
      review: this.description,
      productID:this.data.productId
      
    }
   
    this.BuyerService.ProductSold(product).subscribe(a => {
      this.router.navigate(['/profile']);
    });
    
    this.dialogRef.close(true);
    
  }

}
