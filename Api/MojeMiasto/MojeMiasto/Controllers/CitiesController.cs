using Microsoft.AspNetCore.Mvc;
using MojeMiasto.Data;
using MojeMiasto.Models;
using System.Security.Cryptography;
using System.Text;

namespace MojeMiasto.Controllers
{
    [Route("cities")]
    [ApiController]
    public class CitiesController
    {

        private ILogger<UsersController> _logger;
        private readonly MyDbContext _context;

        public CitiesController(ILogger<UsersController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Route("id/{req_id}")]
        public City GetById(int req_id)
        {
            var data = _context.cities.First(x => x.id == req_id);
            return data;
        }

        [HttpGet]
        [Route("name/{req_name}")]
        public City GetByName(string req_name)
        {
            req_name = req_name.ToLower();
            var data = _context.cities.First(x => x.name == req_name);
            return data;
        }

        [HttpGet]
        [Route("search/{req_name}")]
        public List<City> Search(string req_name)
        {
            req_name = req_name.ToLower();
            var users = _context.cities.Where(x => x.name.StartsWith(req_name)).ToList();
            return users;
        }


        [HttpPost]
        public void AddCity([FromBody] City data)
        {
            if (data == null)
                return;

            data.id = 0;
            data.name = data.name.ToLower();

            _context.cities.Add(data);
            _context.SaveChanges();
        }

    }
}
