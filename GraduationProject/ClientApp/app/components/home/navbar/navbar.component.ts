 import { ChatService } from './../../../services/chat.service';
 import { ChatComponent } from './../../chat/chat.component';
 import { UserService } from '../../../services/user.service';
 import { Component, OnInit } from '@angular/core';

 @Component({
   selector: 'app-navbar',
   templateUrl: './navbar.component.html',
   styleUrls: ['./navbar.component.css']
 })
 export class NavbarComponent implements OnInit {

   constructor(public userService:UserService,public chatService:ChatService) { }

   ngOnInit(): void {
    var interval = setInterval(() => {
       if (this.userService.IsLogin()) {
         this.chatService.GetUserMessage();
         clearInterval(interval);
       }
     },3000)
   
   }
   LogOut() {
     this.chatService.hubConnection.stop().then(a => {
       this.userService.Logout();
    
    });
    
    window.location.reload();
    
  
   }
   ShowDailog(userId: string, UserName: string) {
     this.chatService.GetData(userId);
     document.getElementById("UserName").innerHTML = UserName;
     (<HTMLInputElement>document.getElementById("userId")).value = userId;
     document.getElementById("chat").classList.remove("display-none");
     this.chatService.OnSeeChat(userId).subscribe(a => {
       let container = document.getElementById("scrollContainer");
       container.scrollTo(0, container.scrollHeight);
       this.chatService.newMessage = this.chatService.newMessage.filter(a => a.userId != userId);
     });
   }

 }
