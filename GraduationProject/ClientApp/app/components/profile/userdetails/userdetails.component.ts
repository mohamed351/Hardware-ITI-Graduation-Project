import { ActivatedRoute } from '@angular/router';
import { UserInfoService } from './../../../services/userInfo.service';
import { userInfo } from './../../../models/userInfo';
import { UserService } from './../../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-userdetails',
  templateUrl: './userdetails.component.html',
  styleUrls: ['./userdetails.component.css']
})
export class UserdetailsComponent implements OnInit {
  userID:string = null;
  userInfo:userInfo = null;
  constructor(private UserInfoService:UserInfoService, private activeRouter:ActivatedRoute, public userservice:UserService) { }

  ngOnInit(): void {
    this.activeRouter.params.subscribe(a => {
      if (a.id == null) {
        this.UserInfoService.GetUserInformation().subscribe(a => {
          this.userInfo = a;
        }, (error: HttpErrorResponse)=>{
            console.log(error.error);
            console.log(error.status);
          
        });
      }
      else
      {
        this.userID = a.id;
        this.UserInfoService.GetUserInformationByID(a.id).subscribe(a => {
          this.userInfo = a;
        }, (error: HttpErrorResponse)=>{
            console.log(error.status);
        });
      }
    });
   
   
  }

}
