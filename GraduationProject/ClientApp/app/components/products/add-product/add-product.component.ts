import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

/** @format */

import { SearchElements } from "./../../../models/product";
import { ProductService } from "./../../../services/product.service";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators, FormArray } from "@angular/forms";
import { ImageCroppedEvent } from "ngx-image-cropper";
import { HttpErrorResponse } from '@angular/common/http';
import { ProductValidators} from './add-product.validator';
import { GoogleAnalyticalTrackerService } from 'ClientApp/app/services/google-analytical-tracker.service';

@Component({
	selector: "app-add-product",
	templateUrl: "./add-product.component.html",
	styleUrls: ["./add-product.component.css"],
})
export class AddProductComponent implements OnInit {
	options: SearchElements[] = [];
	filterdProduct: SearchElements[] = [];
	selctedElement: SearchElements = null;
	
	
	constructor(private service: ProductService,
		private ToastrService: ToastrService,
		private router: Router, private google:GoogleAnalyticalTrackerService) { 
		google.eventEmitter("AddProduct", "Product", "click");
	  }

	form = new FormGroup({
		name: new FormControl("", Validators.required),
		description: new FormControl("", Validators.required),
		price: new FormControl("", Validators.required),
		condition: new FormControl("0", Validators.required),
		productId: new FormControl("", [Validators.required,ProductValidators.ValidationOnSelect]),
		images: new FormArray([], Validators.required),
		isNegotiable: new FormControl(false),
		isReplacable: new FormControl(false)
		
	});

	imageChangedEvent: any = "";
	croppedImage: any = "";
	get Name() {
		return this.form.get("name");
	}
	get images() {
		return this.form.get("images")! as FormArray;
	}
	get Product() {
		return this.form.get("productId");
	}
	get IsNegotiable() {
		return this.form.get("isNegotiable");
	}

	get IsReplacable() {
		return this.form.get("isReplacable");
	}

	ngOnInit(): void {}
	OnAddPicture(event) {
		this.imageChangedEvent = event;
		document.getElementById("tansformation").classList.add("abs-form-block");
	}
	imageCropped(event: ImageCroppedEvent) {
		this.croppedImage = event.base64;
	}
	DeleteCropper() {
		document.getElementById("tansformation").classList.remove("abs-form-block");
		(<HTMLInputElement>document.getElementById("image-button")).value = "";
	}
	AddImage = () => {
		let name = (<HTMLInputElement>document.getElementById("image-button"))
			.files[0].name;
		this.images.push(new FormControl({ value: this.croppedImage, name }));
		document.getElementById("tansformation").classList.remove("abs-form-block");
		(<HTMLInputElement>document.getElementById("image-button")).value = "";
		console.log(this.images);
	};
	OnTextWrite() {
		this.filterdProduct = [];
		this.options = [];
		this.service.GetNames(this.Name.value).subscribe((a) => {
			this.options = a;
		});
	}
	OnItemSelected(event) {
	
		this.filterdProduct = this.options.filter(
			(a) => a.name == event.option.value
		);
	}
	DeleteImage(event) {
	  (<FormArray>this.images.value) =this.images.value.filter(a => a != event);

	}
  OnSelectChange(event) {
    this.selctedElement = this.filterdProduct.find(a => a.productId == event.target.value);
    console.log(this.selctedElement);
    
  }

	OnFormSubmit() {
		console.log(this.form.value);
		this.service.AddProduct(this.form.value).subscribe(a => {
			this.ToastrService.success("Product Successfully Added", "Successful");
			this.router.navigate(['']);
		}, (error: HttpErrorResponse)=>{
				this.ToastrService.error(error.message);
	  })
    console.log(this.form.value);
  }

	imageLoaded() {}
	cropperReady() {}
	loadImageFailed() {}
}
