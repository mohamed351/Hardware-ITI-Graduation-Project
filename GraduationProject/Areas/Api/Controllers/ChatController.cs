using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.ExtenstionMethods;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chat;
        private readonly IMapper _mapper;

        public ChatController(IChatRepository chat,IMapper mapper)
        {
            this._chat = chat;
            this._mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("ChatApplication/{Id}")]
        public IActionResult GetChat(string Id)
        {
            string userId = User.GetUserIdToken();
           var elements = _mapper.Map<IEnumerable<ChatMessages>,IEnumerable<ChatMessageViewModel>>(_chat.GetChatMessages(userId, Id));
            return Ok(elements);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ChatApplication/GetUnReadUsers")]
        public IActionResult GetUnReadUsers()
        {
            string UserID = User.GetUserIdToken();
            var model = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UsersUnReadMessageViewModel>>(_chat.GetUserUnReadedMessages(UserID));
            return Ok(model);

        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ChatApplication/SetRead/{id}")]
        public IActionResult SetToRead(string id)
        {
            string UserID = User.GetUserIdToken();
            _chat.SetToRead(UserID, id);
            _chat.SaveAll();
            return NoContent();
        }



    }
}