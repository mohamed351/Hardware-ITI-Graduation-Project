import { Areas } from './../models/areas';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AreasService {

  constructor(private http: HttpClient) {
    
  }
  GetAreas(id:number) {
    return this.http.get<Areas[]>("/api/Areas/?ID="+id);
  }
}
