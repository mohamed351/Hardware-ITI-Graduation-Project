import { UserService } from './../../services/user.service';
import { ChatService } from './../../services/chat.service';
import { Component, OnInit } from '@angular/core';
import { chatMessage } from 'ClientApp/app/models/chat';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  constructor(public ChatService:ChatService,public UserService:UserService) { }

  ngOnInit(): void {
  
  var interval= setInterval(()=> {
      if (this.UserService.IsLogin()) {
        this.ChatService.startConnection();
        clearInterval(interval);
      }
      else
      {
       
      
      }
      
    }, 5000);
  }
  closeChat() {
    document.getElementById("chat").classList.add("display-none");
    
  }
  RenderSenderandReciverClass(status:boolean) {
    if (status == true)
      return 'row msg_container base_sent';
    else
      return 'msg_container base_receive';
    
  }
  SendData(data: HTMLInputElement, message) {
    this.ChatService.SendMessage(message.value, data.value);
    let messageChat = new chatMessage();
    messageChat.message = message.value;
    messageChat.sourceID = this.UserService.CurrentUserId();
    this.ChatService.chatMessage.push(messageChat);
    (<HTMLInputElement>document.getElementById("btn-input")).value = "";

  }
  resizeEvent() {
   
  }

}
