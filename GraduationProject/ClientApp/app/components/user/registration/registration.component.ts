import { Router } from '@angular/router';
/** @format */

import { Registration } from "../../../models/registration";
import { RegistrationService } from '../../../services/registration.service';
import { City } from "../../../models/city";
import { CitiesService } from "../../../services/cities.service";
import { AreasService } from "../../../services/areas.service";
import { Areas } from "../../../models/areas";
import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
	selector: "app-registration",
	templateUrl: "./registration.component.html",
	styleUrls: ["./registration.component.css"],
})
export class RegistrationComponent implements OnInit {
	areas: Areas[];
	cities: City[];
	constructor(
		private areaService: AreasService,
		private citiesServices: CitiesService,
    private register: RegistrationService,
		private toastr: ToastrService,
	private router:Router
	) {}
	passwordConfirmation: boolean = false;
	ngOnInit(): void {
		this.citiesServices.GetCities().subscribe((a) => {
			this.cities = a;
		});
	}
	OnRegistration(data: NgForm) {
		if (data.valid) {
			console.log(data.value);
			this.register.RegisterUser(data.value).subscribe((a) => {
				this.toastr.success("Account Successfully Created.");
				this.router.navigate(['/Login']);
			    
      }, (err: HttpErrorResponse) => {
          console.log(err);
          this.toastr.error(err.error);    
      });
		}
	}
	OComparePasswords(password, confirmPassword) {
		console.log(confirmPassword.value);
		console.log(password.value);
		this.passwordConfirmation = password.value != confirmPassword.value;
		console.log(this.passwordConfirmation);
	}
	OnCityChange(data) {
		if (data.value != 0) {
			this.areaService.GetAreas(data.value).subscribe((a) => {
				this.areas = a;
				console.log(a);
			});
		} else {
			this.areas = [];
		}
	}
	onChange(data) {
		console.log(data);
	}
}
