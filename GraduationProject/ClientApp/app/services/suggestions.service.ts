import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Suggestion } from '../models/suggestion';

@Injectable({
  providedIn: 'root'
})
export class SuggestionsService {

    constructor(private http: HttpClient) { }

    AddSuggestion(formData: Suggestion) {
        return this.http.post<Suggestion>("api/UserInformation/AddSuggestion", formData);
    }
}
