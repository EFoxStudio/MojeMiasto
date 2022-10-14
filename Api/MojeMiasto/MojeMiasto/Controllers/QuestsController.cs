﻿using Microsoft.AspNetCore.Mvc;
using MojeMiasto.Data;
using MojeMiasto.Models;

namespace MojeMiasto.Controllers
{
    [Route("quests")]
    [ApiController]
    public class QuestsController
    {
        private ILogger<UsersController> _logger;
        private readonly MyDbContext _context;

        public QuestsController(ILogger<UsersController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("id/{req_id}")]
        public Quest GetById(int req_id)
        {
            var user = _context.quests.First(x => x.id == req_id);
            return user;
        }

        [HttpGet]
        [Route("city_id/{req_city_id}")]
        public List<Quest> GetByCity(int req_city_id)
        {
            var users = _context.quests.Where(x => x.city_id == req_city_id && x.end_date >= DateTime.Now).ToList();
            return users;
        }

        [HttpGet]
        [Route("district_id/{req_district_id}")]
        public List<Quest> GetByDistricts(int req_district_id)
        {
            var users = _context.quests.Where(x => x.district_id == req_district_id && x.end_date >= DateTime.Now).ToList();
            return users;
        }

        [HttpPost]
        public void AddUser([FromBody] Quest data)
        {
            if (data == null)
                return;

            data.id = 0;
            _context.quests.Add(data);
            _context.SaveChanges();
        }

        [HttpPut]
        public async void UpdateUser([FromBody] Quest data)
        {
            if (data == null)
                return;

            Quest old = _context.quests.First(x => x.id == data.id);
            if (await _context.quests.FindAsync(data.id) is Quest found)
            {
                _context.Entry(found).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
            }
        }


        [HttpDelete]
        [Route("delete/{req_id}")]
        public async void DeleteUser(int req_id)
        {
            var data = _context.quests.First(x => x.id == req_id);
            _context.quests.Remove(data);
            _context.SaveChanges();
        }
    }
}
