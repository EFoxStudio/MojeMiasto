using Microsoft.AspNetCore.Mvc;
using MojeMiasto.Data;
using MojeMiasto.Models;

namespace MojeMiasto.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController
    {
        private ILogger<UsersController> _logger;
        private readonly MyDbContext _context;

        public UsersController(ILogger<UsersController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("id/{req_id}")]
        public User GetById(int req_id)
        {
            var user = _context.users.First(x => x.id == req_id);
            return user;
        }

        [HttpGet]
        [Route("email/{req_email}")]
        public User GetByEmail(string req_email)
        {
            var user = _context.users.First(x => x.email == req_email);
            return user;
        }

        [HttpPost]
        [Route("new")]
        public void AddUser([FromBody] User data)
        {
            if (data == null)
                return;

            data.id = 0;
            _context.users.Add(data);
        }


    }
}
