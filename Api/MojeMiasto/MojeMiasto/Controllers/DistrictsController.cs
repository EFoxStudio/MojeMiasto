using Microsoft.AspNetCore.Mvc;
using MojeMiasto.Data;
using MojeMiasto.Models;

namespace MojeMiasto.Controllers
{
    [Route("districts")]
    [ApiController]
    public class DistrictsController
    {
        private ILogger<UsersController> _logger;
        private readonly MyDbContext _context;

        public DistrictsController(ILogger<UsersController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Route("id/{req_id}")]
        public District GetById(int req_id)
        {
            var data = _context.districts.First(x => x.id == req_id);
            return data;
        }

        [HttpGet]
        [Route("name/{req_name}")]
        public District GetByName(string req_name)
        {
            req_name = req_name.ToLower();
            var data = _context.districts.First(x => x.name == req_name);
            return data;
        }

        [HttpGet]
        [Route("city_id/{req_city_id}/search/{req_name}")]
        public List<District> Search(string req_name, int req_city_id)
        {
            req_name = req_name.ToLower();
            var users = _context.districts.Where(x => x.city_id == req_city_id &&   x.name.StartsWith(req_name)).ToList();
            return users;
        }

        [HttpPost]
        public void AddCity([FromBody] District data)
        {
            if (data == null)
                return;

            data.id = 0;
            data.name = data.name.ToLower();

            _context.districts.Add(data);
            _context.SaveChanges();
        }
    }
}
