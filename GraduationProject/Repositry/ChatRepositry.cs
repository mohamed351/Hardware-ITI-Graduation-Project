using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class ChatRepositry:Repositry<ChatMessages,int>,IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepositry(ApplicationDbContext context)
            :base(context)
        {
            this._context = context;
        }

        public IEnumerable<ChatMessages> GetChatMessages(string sourceId, string destination)
        {
          return  Context.ChatMessages
                .Include(a => a.SourceUser)
                .Include(a => a.DestinationUser)
                .Where(a => (a.SourceUserId == sourceId && a.DestinationUserID == destination) || (a.SourceUserId == destination && a.DestinationUserID == sourceId));

               
        }

        public IEnumerable<ApplicationUser> GetUserUnReadedMessages(string UserID)
        {
            return Context.ChatMessages
                 .Include(a => a.SourceUser)
                 .Where(a => a.DestinationUserID == UserID && a.IsSeen == false)
                 .Select(a => a.SourceUser).Distinct();

        }

        public void SetToRead(string UserID, string destinationUser)
        {
          var model =   Context.ChatMessages
                .Where(a => a.IsSeen == false && a.SourceUserId == destinationUser && a.DestinationUserID == UserID)
                .ToList();
            foreach (var item in model)
            {
                item.IsSeen = true;
            }
           
        }
    }
}
