using Microsoft.AspNetCore.Mvc;
using MojeMiasto.Data;
using MojeMiasto.Models;

namespace MojeMiasto.Controllers
{
    [Route("messages")]
    [ApiController]
    public class MessagesController
    {

        private ILogger<UsersController> _logger;
        private readonly MyDbContext _context;

        public MessagesController(ILogger<UsersController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("id/{req_id}")]
        public Message GetById(int req_id)
        {
            var data = _context.messages.First(x => x.id == req_id);
            return data;
        }

        [HttpGet]
        [Route("chat_id/{req_chat_id}")]
        public List<Message> GetByChat(int req_chat_id)
        {
            var data = _context.messages.Where(x => x.chat_id == req_chat_id).ToList();
            return data;
        }

        [HttpPost]
        public void AddUser([FromBody] Message data)
        {
            if (data == null)
                return;

            data.id = 0;
            _context.messages.Add(data);
            _context.SaveChanges();
        }

        [HttpPut]
        public async void UpdateUser([FromBody] Message data)
        {
            if (data == null)
                return;

            Message old = _context.messages.First(x => x.id == data.id);
            if (await _context.messages.FindAsync(data.id) is Message found)
            {
                _context.Entry(found).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
            }
        }


        [HttpDelete]
        [Route("delete/{req_id}")]
        public async void DeleteUser(int req_id)
        {
            var data = _context.messages.First(x => x.id == req_id);
            _context.messages.Remove(data);
            _context.SaveChanges();
        }
    }
}
