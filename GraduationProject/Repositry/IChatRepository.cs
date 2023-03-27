using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
   public interface IChatRepository:IRepositry<ChatMessages, int>
    {
        public IEnumerable<ChatMessages> GetChatMessages(string sourceId, string destination);
        public IEnumerable<ApplicationUser> GetUserUnReadedMessages(string UserID);
        public void SetToRead(string UserID, string destinationUser);

       

       
    }
}
