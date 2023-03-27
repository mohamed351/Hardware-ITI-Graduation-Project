import { Reviews } from "./../models/reviews";
import { UserService } from "./user.service";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { Login } from "../models/login";
import { userInfo } from "../models/userInfo";

@Injectable({
	providedIn: "root",
})
export class UserInfoService {
	constructor(private http: HttpClient, private UserService: UserService) {}

	GetUserInformation() {
		return this.http.get<userInfo>("/api/Account/UserInfo");
	}
	GetUserReviews() {
		return this.http.get<Reviews[]>("/api/Account/UserReviews");
	}
	GetUserInformationByID(UserId: string) {
		return this.http.get<userInfo>(`/api/UserInformation/GetUserInfo/${UserId}`);
	}
	GetUserReviewById(UserId: string) {
		return this.http.get<Reviews[]>(`/api/UserInformation/UserReviews/${UserId}`);
	}
	GetUserRatingById(UserId: string) {
		return this.http.get(`/api/UserInformation/UserRating/${UserId}`);
	}
	GetNumberOfSoldItems(id: string) {
		return this.http.get<number>('api/Product/GetNumberOfSoldItems/' + id);
    }

}
