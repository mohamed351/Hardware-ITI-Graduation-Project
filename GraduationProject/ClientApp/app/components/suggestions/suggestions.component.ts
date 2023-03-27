import { Component, OnInit } from '@angular/core';
import { SuggestionsService } from '../../services/suggestions.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-suggestions',
  templateUrl: './suggestions.component.html',
  styleUrls: ['./suggestions.component.css']
})
export class SuggestionsComponent implements OnInit {

    constructor(private suggestionService: SuggestionsService,
        private router: Router, private ToastrService: ToastrService) { }
    Save(f) {
        this.suggestionService.AddSuggestion(f.value)
            .subscribe(a => {
                this.ToastrService.success("Thank You for your Suggestion!", "Successful");
                this.router.navigate(['/']);
            });
    }
  ngOnInit(): void {
  }

}
