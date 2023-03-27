import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductChart } from '../models/productchart';

@Injectable({
  providedIn: 'root'
})
export class ChartsService {

  constructor(private http: HttpClient) {
    
  }
  GetPriceStatistics(id: number) {
   
    return this.http.get<ProductChart>("/api/Chart/" + id);
  }
}
