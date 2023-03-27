import { Component, OnInit } from '@angular/core';
import { UserSettingsService } from 'ClientApp/app/services/usersettings.service';
import { UserDetails } from 'ClientApp/app/models/userDetails';
import { City } from "../../../models/city";
import { CitiesService } from "../../../services/cities.service";
import { AreasService } from "../../../services/areas.service";
import { Areas } from "../../../models/areas";
import { UserSettings } from 'ClientApp/app/models/userSettings';
import { HttpError } from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-usersettings',
  templateUrl: './usersettings.component.html',
  styleUrls: ['./usersettings.component.css']
})

export class UserSettingsComponent implements OnInit {
  UserDetailsViewModel: UserDetails;
  UserSettingsViewModel: UserSettings;
  EmailConfirmation: boolean = false;
  PasswordConfirmation: boolean = false;
  areas: Areas[];
  cities: City[]; 
  constructor(
    private UserSettingsService: UserSettingsService,
    private areaService: AreasService,
    private citiesServices: CitiesService,
    private toaster: ToastrService) {
      this.UserSettingsViewModel = new UserSettings();
   }

  ngOnInit(): void {
    this.UserSettingsService.GetUserDetails().subscribe(
      data =>{
        this.UserDetailsViewModel = data;
      }
    );

    this.citiesServices.GetCities().subscribe((a) => {
			this.cities = a;
		})
  }
  
  ChangeEmail(email:string){
    this.UserSettingsViewModel.email = email;
    this.UserSettingsService.ChangeEmail(this.UserSettingsViewModel).subscribe(a=>{
      this.toaster.success(a.message);
      this.ngOnInit();
    },(error:HttpError)=>{
      this.toaster.error("Email Update was Unsuccessful.");
      console.log(error.statusCode)
    });
  }

  ChangePassword(currentpassword:string ,password:string){
    this.UserSettingsViewModel.currentPassword = currentpassword;
    this.UserSettingsViewModel.password = password;
    this.UserSettingsService.ChangePassword(this.UserSettingsViewModel).subscribe(a=>{
      this.toaster.success(a.message);
      this.ngOnInit();
    },(error:HttpError)=>{
      this.toaster.error("Password Update was Unsuccessful.");
      console.log(error.statusCode)
    });
  }

  ChangeAddress(address: string){
    this.UserSettingsViewModel.address = address;
    this.UserSettingsService.ChangeAddress(this.UserSettingsViewModel).subscribe(a=>{
      this.toaster.success(a.message);
      this.ngOnInit();
    },(error:HttpError)=>{
      this.toaster.error("Address Update was Unsuccessful.");
      console.log(error.statusCode)
    });
  }

  ChangeUsername(username: string){
    this.UserSettingsViewModel.username = username;
    this.UserSettingsService.ChangeUser(this.UserSettingsViewModel).subscribe(a=>{
      this.toaster.success(a.message);
      this.ngOnInit();
    },(error:HttpError)=>{
      this.toaster.error("Username Update was Unsuccessful.");
      console.log(error.statusCode)
    });
  }

  ChangePhone(phone: string){
    this.UserSettingsViewModel.phoneNumber = phone;
    this.UserSettingsService.ChangePhone(this.UserSettingsViewModel).subscribe(a=>{
      this.toaster.success(a.message);
      this.ngOnInit();
    },(error:HttpError)=>{
      this.toaster.error("Phone Update was Unsuccessful.");
      console.log(error.statusCode)
    });
  }

  ChangeArea(areaid: number){
    this.UserSettingsViewModel.areaID = areaid;
    console.log(areaid);
    console.log(this.UserSettingsViewModel.areaID);
    this.UserSettingsService.ChangeArea(this.UserSettingsViewModel).subscribe(a=>{
      this.toaster.success(a.message);
      this.ngOnInit();
    },(error:HttpError)=>{
      this.toaster.error("Area Update was Unsuccessful.");
      console.log(error.statusCode)
    });
  }


  OnCityChange(data) {
		
			this.areaService.GetAreas(data.value).subscribe((a) => {
				this.areas = a;
			});
  }

  CompareEmails(email, emailConfirmation) {
    this.EmailConfirmation = (email.value == emailConfirmation.value) ? true : false;
	}
  ComparePasswords(password, passwordConfirmation) {
    this.PasswordConfirmation = (password.value == passwordConfirmation.value) ? true : false;
	}

  
}
