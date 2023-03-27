import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserSettings } from '../models/userSettings';
import { UserDetails } from '../models/userDetails';



@Injectable({
  providedIn: 'root'
})
export class UserSettingsService {

  constructor(private http: HttpClient) { 
  }

  GetUserDetails() {
   return this.http.get<UserDetails>("/api/Profile/Edit/UserDetails");
  }

  ChangeUser(userSettings:UserSettings) {
    return this.http.post<any>("/api/Profile/Edit/Username",userSettings);
  }

  ChangeEmail(userSettings:UserSettings) {
    return this.http.post<any>("/api/Profile/Edit/Email",userSettings);
  }

  ChangePassword(userSettings:UserSettings) {
   return this.http.post<any>("/api/Profile/Edit/Password",userSettings);
  }

  ChangePhone(userSettings:UserSettings) {
   return this.http.post<any>("/api/Profile/Edit/Phone",userSettings);
  }

  ChangeAddress(userSettings:UserSettings) {
   return this.http.post<any>("/api/Profile/Edit/Address",userSettings);
  }

  ChangeArea(userSettings:UserSettings) {
   return this.http.post<any>("/api/Profile/Edit/Area",userSettings);
  }
}
