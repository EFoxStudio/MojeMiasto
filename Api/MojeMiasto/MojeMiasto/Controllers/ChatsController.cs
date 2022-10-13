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

        

    }
}
