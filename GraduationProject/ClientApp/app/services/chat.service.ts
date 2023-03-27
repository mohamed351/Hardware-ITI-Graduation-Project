import { UserService } from './user.service';
import { chatMessage } from './../models/chat';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalr from '@aspnet/signalr';
import { IHttpConnectionOptions } from '@aspnet/signalr';
import { newUserMessage } from '../models/newUserMessages';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public chatMessage: chatMessage[] = [];
  public newMessage: newUserMessage[] = [];
  public hubConnection: signalr.HubConnection;

  constructor(private http: HttpClient, private UserService: UserService) {

    const options: IHttpConnectionOptions = {
      transport: signalr.HttpTransportType.LongPolling,
      skipNegotiation: false,
      accessTokenFactory: () => {
        return this.UserService.CurrentUserToken();
      }
    };
    this.hubConnection = new signalr.HubConnectionBuilder()
      .configureLogging(signalr.LogLevel.Information)
      .withUrl("chathub",options)
      .build();
     
  }
  GetData(Id:string) {
    return this.http.get("/api/Chat/ChatApplication/" + Id)
      .toPromise()
      .then(a => this.chatMessage = a as chatMessage[])
      .then(() => {
        let container = document.getElementById("scrollContainer");
        container.scrollTo(0, container.scrollHeight);
      });
    
    
        
     
  }
  public startConnection = () => {
    this.hubConnection.start()
      .then(() => console.log("connection started"))
      .catch(error => console.log(error));
  }
  public SendMessage = (message:string , userId:String) => {
    this.hubConnection.invoke("SendMessage",message,userId);
  }
  public GetUserMessage() {
    return this.http.get("/api/Chat/ChatApplication/GetUnReadUsers")
      .toPromise().then(a => { this.newMessage = a as newUserMessage[]; });
  }
  public OnSeeChat(userID:string) {
    return this.http.put("/api/Chat/ChatApplication/SetRead/" + userID,null);
  }
  public Status() {
    return this.hubConnection.state;
  }
  

}
