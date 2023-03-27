import { BuyerService } from './../../../services/buyer.service';
import { Component, OnInit } from '@angular/core';
import { BuyersReview } from 'ClientApp/app/models/BuyersReview';
import { RatingDailogComponent } from '../../resuable/rating-dailog/rating-dailog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-user-reviews',
  templateUrl: './user-reviews.component.html',
  styleUrls: ['./user-reviews.component.css']
})
export class UserReviewsComponent implements OnInit {
  buyersReview:BuyersReview[] =[]
  constructor(private BuyerService:BuyerService,public dialog: MatDialog) { }

  ngOnInit(): void {
    this.BuyerService.GetUserProductToReview().subscribe(a => {
      this.buyersReview = a;
      console.log(this.buyersReview);
    });
  }
  ReviewNow(userId, userName,productId) {
  
    let dialogRef = this.dialog.open(RatingDailogComponent, { data: { userId, userName, productId: productId } });
    dialogRef.afterClosed().subscribe(a => {
       
      if (a == true) {
        window.location.reload();
      }
    });
  }


}
