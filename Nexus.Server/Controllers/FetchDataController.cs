using Microsoft.AspNetCore.Mvc;
using Nexus.Server.Entity;
using Nexus.Server.Models;
using NexusServergRPC;
namespace Nexus.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FetchDataController : ControllerBase
    {
        [Route("getContacts/{username}")]
        [HttpGet]
        public async Task<List<Contact_Model>?> GetContacts(string userName)
        {
            List<Contact_Model> contacts = new List<Contact_Model>();

            var list = await ServerController.ListenerX.GetContactsRPC(userName);
            foreach (var item in list)
            {
                contacts.Add(new Contact_Model
                {
                    LastMessage = item.LastMessage,
                    UserName = item.UserName,
                    UserAvatar = item.UserAvatar.ToArray(),
                });
            }
            return contacts;
        }


        [Route("getMessages/{sender}/{receiver}")]
        [HttpGet]
        public async Task<List<Message>> GetMessages()
        {
            var messages = new List<Message>();

            return messages;
        }


        //[Route("getMessages")]
        //[HttpGet]
        //public async Task<List<Message>> ListenMessages()
        //{
            
        //    return messages;
        //}


        [Route("getCurrentUser")]
        [HttpGet]
        public async Task<CurrentUser_Model> GetCurrentUser()
        {
            return await ServerController.ListenerX.GetCurrentUserRPC();
        }

    }
}
