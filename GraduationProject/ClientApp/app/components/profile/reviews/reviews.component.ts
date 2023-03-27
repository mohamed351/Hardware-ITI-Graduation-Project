import { UserInfoService } from './../../../services/userInfo.service';
import { Reviews } from '../../../models/reviews';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  review: Reviews[] = null;
  constructor(private userInfo:UserInfoService, private activateRouter:ActivatedRoute) { }

  ngOnInit(): void {
    this.activateRouter.params.subscribe(a => {
      if (a.id == null) {
        this.userInfo.GetUserReviews().subscribe(a => {
          this.review = a;
       
        });
      }
      else
      {
        this.userInfo.GetUserReviewById(a.id).subscribe(a => {
          this.review = a;
         
        });  
      }
    });
  
  }

}
