using Microsoft.AspNetCore.Mvc;
using MojeMiasto.Data;
using MojeMiasto.Models;

namespace MojeMiasto.Controllers
{
    [Route("chats")]
    [ApiController]
    public class ChatsController
    {
        private ILogger<UsersController> _logger;
        private readonly MyDbContext _context;

        public ChatsController(ILogger<UsersController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("id/{req_id}")]
        public Chat GetById(int req_id)
        {
            var data = _context.chats.First(x => x.id == req_id);
            return data;
        }


        [HttpGet]
        [Route("user_id/{req_id}")]
        public List<Chat> GetByUserId(int req_id)
        {
            var users = _context.chats.Where(x => x.user1_id == req_id || x.user2_id == req_id).ToList();
            return users;
        }


        [HttpPost]
        public void AddCity([FromBody] Chat data)
        {
            if (data == null)
                return;

            data.id = 0;

            _context.chats.Add(data);
            _context.SaveChanges();
        }

    }
}
