import { Component, OnInit, SimpleChanges } from '@angular/core';
import { ProductService } from 'ClientApp/app/services/product.service';
import { ActivatedRoute } from '@angular/router';
import { filterAttribute } from 'ClientApp/app/models/filterAttribute';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent implements OnInit {
  categoryId;
  filterAttribute;
  brand;
  condition;
  fromPrice;
  toPrice;
  isOn;

  constructor(private productService: ProductService, private route: ActivatedRoute) {

  }

  check() {
    this.route.params.subscribe(params => {
      this.categoryId = params["categoryId"];
      console.log(this.categoryId);
      debugger;
      this.productService.GetFilters(params["categoryId"]).subscribe(
        (data => {
          this.filterAttribute = data;
          console.log(data);
          //this.test_title = data['TitleName'];
          //this.pla = this.test_title[0];
          //console.log("test title", this.pla); 
        }
        ));
      console.log("after function", this.categoryId);
    });
  }

  ngOnInit(): void {
    debugger;
    this.check();
    // this.productService.GetFilters(2).then(data => {
    //this.ev = event;
    //console.log(event); // Has a value
    //console.log(this.ev); // Has a value
    // });
  }
  onFilter(){
    console.log(this.filterAttribute);
  }

  // ngOnChanges(changes: SimpleChanges) {
  //   if (changes['filterAttribute'].currentValue) {
  //       this.filterAttribute = filterAttribute
  //   }
  //}

}
