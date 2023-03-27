using AutoMapper;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.ExtenstionMethods;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Hubs
{
    public class ConnectionUserID: IConnectionUserID
    {
        public string UserID { get; set; }
        public string ConnectionID { get; set; }
    }
    public interface IConnectionUserID
    {
        public string UserID { get; set; }
        public string ConnectionID { get; set; }

    }



    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub:Hub
    {
        
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext context;
        private readonly List<ConnectionUserID> _connectionUserIDs;

        public ChatHub(IChatRepository chatRepository
            ,IMapper mapper, ApplicationDbContext context,List<ConnectionUserID> connectionUserID)
        {
            
            this._chatRepository = chatRepository;
            this._mapper = mapper;
            this.context = context;
            this._connectionUserIDs = connectionUserID;
        }
    
        public async Task SendMessage(string message, string UserID)
        {
            string ID = Context.User.GetUserIdToken();
            string Name = Context.User.GetUserName();
           
            var chat = new Models.ChatMessages()
            {
                Message = message,
                DestinationUserID = UserID,
                SourceUserId = ID,
                DateTimeOfMessage = DateTime.Now,
                IsSeen =false

            };
            ApplicationUser user = context.Users.SingleOrDefault(a => a.Id == UserID);
            _chatRepository.Add(chat);
            _chatRepository.SaveAll();
            List<ConnectionUserID> connectionUserID = _connectionUserIDs.Where(a => a.UserID == UserID).ToList();
            if (connectionUserID != null)
            {
               var model = _mapper.Map<ChatMessages, ChatMessageViewModel>(chat);
                ApplicationUser user1 = context.Users.FirstOrDefault(a => a.Id == ID);
                model.SourceName =  $"{user1.FirstName} {user1.LastName}";
                foreach (var item in connectionUserID)
                {
                    await Clients.Client(item.ConnectionID).SendAsync("ReciveMessage", model);
                }
               
              
            }  
         
        }
        public override Task OnConnectedAsync()
        {
            _connectionUserIDs.Add(new ConnectionUserID()
            {
                ConnectionID = base.Context.ConnectionId,
                UserID = base.Context.User.GetUserIdToken()
            });

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionUserID connection = _connectionUserIDs.SingleOrDefault(a => a.ConnectionID == base.Context.ConnectionId);
            _connectionUserIDs.Remove(connection);
            return Task.CompletedTask;
           
        }
      
    }
}
