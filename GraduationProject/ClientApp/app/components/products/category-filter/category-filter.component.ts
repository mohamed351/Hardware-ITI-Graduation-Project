import { Component, OnInit, Renderer2, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { category } from 'ClientApp/app/models/Category';
import { UserProduct } from '../../../models/user-product';
//import { Route } from '@angular/compiler/src/core';
import { Router, ActivatedRoute } from '@angular/router';
import { filterAttribute } from 'ClientApp/app/models/filterAttribute';
import { filterDetails } from 'ClientApp/app/models/FilterDetails';
//import {  } from 'events';

@Component({
  selector: 'app-category-filter',
  templateUrl: './category-filter.component.html',
  styleUrls: ['./category-filter.component.css']
})
export class CategoryFilterComponent implements OnInit {
  @Output() filterproducts = new EventEmitter();

  categories: category;
  products: UserProduct[] = [];
  catId;
  isOn: boolean = true;
  filterAttribute;
  filterDetails: filterDetails;
  Brand: string[];
  Condition: string[];
  FromPrice: number;
  ToPrice: number;
  Rating: number;
  check: boolean = true;

  constructor(private productService: ProductService, private route: Router, private render: Renderer2) {


  }
  rateVal(filter, val) {
    //var fstar = document.getElementById("'"+val+"'");
    filter.selectedRating = val;
    var i = 1;
    for (; i <= val; i++) {
      var star = document.getElementById("star" + i);
      star.className = "fa fa-star checked";
    }
    for (; i <= 5; i++) {
      var star = document.getElementById("star" + i);
      star.className = "fa fa-star";
    }
    this.onFilter();
  }

  FilterCategory(catId) {
    console.log("clicked");
    this.isOn = false;
    console.log(this.isOn);
    //this.productService.GetFilters(categoryId);
    this.productService.GetFilters(catId).subscribe(
      (data => {
        this.filterAttribute = data;

        this.filterAttribute.forEach(filter => {
          if (filter.FilterType == "StaticChoice" || filter.FilterType == "DynamicChoice") {
            filter.ChoicesToRender = []
            filter.Choices.forEach(element => {
              filter.ChoicesToRender.push({
                "name": element
              })
            });
          }
        });
        console.log(data);
        this.catId = catId;
        this.onFilter();
      }
      ));
  }

  arr: Array<any> = new Array(5);

  onFilter() {
    var queryParams: string = ""
    this.filterAttribute.forEach(element => {
      if (element.FilterType == "From-To") {
        if (element.From) {
          queryParams += "FromPrice=" + element.From + "&";
        }
        if (element.To) {
          queryParams += "ToPrice=" + element.To + "&";
        }
      }

      if (element.FilterType == "StaticChoice" || element.FilterType == "DynamicChoice") {
        var queryParamKey = "";
        if (element.FilterType == "StaticChoice") {
          queryParamKey = "Condition";
        } else {
          queryParamKey = "Brand";
        }
        element.ChoicesToRender.forEach(choice => {
          if ("selected" in choice && choice.selected) {
            queryParams += queryParamKey + "=" + choice.name + "&";
          }
        });
      }

      console.log(element);

      if (element.FilterType == "Rating" && element.selectedRating != undefined) {
        queryParams += "Rating=" + element.selectedRating + "&";
      }
    });

    queryParams += "CategoryId=" + this.catId;

    console.log(queryParams);

    this.productService.GetFilterProducts(queryParams)
      .subscribe(data => {
        this.products = data;
        this.filterproducts.emit(data);
      })
  }


  ngOnInit(): void {
    this.productService.GetAllCategories().subscribe(data => {
      this.categories = data;
      console.log(data);

      this.productService.GetAllProducts()
        .subscribe(data => {
          this.products = data;
          console.log(data);
        })
    })
  }

}
