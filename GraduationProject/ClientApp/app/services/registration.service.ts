import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Registration } from '../models/registration';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient) { 
    
  }

  RegisterUser(registerUser:Registration) {
   return this.http.post<Registration>("/api/Account/Register",registerUser);
  }
}
