import { newUserMessage } from './models/newUserMessages';
import { ChatService } from './services/chat.service';
import { UserService } from './services/user.service';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { animation } from '@angular/animations';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
 
})
export class AppComponent {
  title = 'myapp';
  
  constructor(public UserService:UserService,public ChatService:ChatService) {
    this.ChatService.hubConnection.on("ReciveMessage", (data) => {
  
      if (document.getElementById("chat").classList.contains("display-none") || (<HTMLInputElement>document.getElementById("userId")).value != data.sourceID) {
        let newMessage = new newUserMessage();
        newMessage.userId = data.sourceID;
        newMessage.name = data.sourceName;
        this.ChatService.newMessage.push(newMessage);
         
      }
      else if(!document.getElementById("chat").classList.contains("display-none"))
      {
       
        this.ChatService.chatMessage.push(data);
        this.ChatService.OnSeeChat(data.sourceID).subscribe(a => {
          let container = document.getElementById("scrollContainer");
          container.scrollTo(0, container.scrollHeight);
        });
        

      }
      
    });
    
  }

}
