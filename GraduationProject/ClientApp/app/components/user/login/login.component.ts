import { ChatService } from './../../../services/chat.service';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    notLogin: boolean = false;
    constructor(private userService: UserService, private router: Router,
        private route: ActivatedRoute,private ChatService:ChatService) { }

    Save(formData) {
        this.userService.userAuthentication(formData.value).subscribe((data: any) => {
            console.log('Login Data ', data);
            localStorage.setItem('token', data.token);
            localStorage.setItem('userId', data.userId);
            localStorage.setItem('userName', data.userName);
            //localStorage.setItem('userRole', data.Role);

            let returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
            this.router.navigate([returnUrl || '/']);
            this.ChatService.startConnection();
        },
            (err: HttpErrorResponse) => {
                this.notLogin = true;
            });
    }

  ngOnInit(): void {
  }

}
