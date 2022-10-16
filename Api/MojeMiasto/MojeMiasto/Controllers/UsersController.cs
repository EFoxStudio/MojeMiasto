using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeMiasto.Data;
using MojeMiasto.Models;
using System.Security.Cryptography;
using System.Text;

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
            req_email = req_email.ToLower();
            var user = _context.users.First(x => x.email == req_email);
            return user;
        }

        [HttpGet]
        [Route("city_id/{req_city_id}")]
        public List<User> GetByCity(int req_city_id)
        {
            var users = _context.users.Where(x => x.city_id == req_city_id).ToList();
            return users;
        }

        [HttpGet]
        [Route("district_id/{req_district_id}")]
        public List<User> GetByDistricts(int req_district_id)
        {
            var users = _context.users.Where(x => x.district_id == req_district_id).ToList();
            return users;
        }

        [HttpGet]
        [Route("icon/{req_id}")]
        public string GetIconById(int req_id)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "icons/");
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(path);
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(req_id + ".*");

            if (filesInDir.Length <= 0)
                return "0.png";

            return filesInDir[0].Name;
        }

        [HttpPost]
        public void AddUser([FromBody] User data)
        {
            if (data == null)
                return;

            data.id = 0;
            data.password = HashPassword(data.password);
            data.email = data.email.ToLower();

            _context.users.Add(data);
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("icon/{user_id}")]
        public void AddUserIcon(int user_id, [FromForm] IconUpload file)
        {
            using (FileStream fileStream
                = System.IO.File.Create(
                    Path.Combine(Directory.GetCurrentDirectory(),
                    "icons/") + user_id.ToString() + ".png"))
            {
                file.files.CopyTo(fileStream);
                fileStream.Flush();
            }
        }

        [HttpPost]
        [Route("hash")]
        public string HashPassword([FromBody] string password)
        {
            SHA256 hash = SHA256.Create();

            var passBytes = Encoding.Default.GetBytes(password);
            var hashedPass = hash.ComputeHash(passBytes);

            return Convert.ToHexString(hashedPass);
        }

        [HttpPut]
        public async void UpdateUser([FromBody] User data)
        {
            if (data == null)
                return;

            User old = _context.users.First(x => x.id == data.id);

            if(data.password != old.password)
                data.password = HashPassword(data.password);

            data.email = data.email.ToLower();

            if (await _context.users.FindAsync(data.id) is User found)
            {
                _context.Entry(found).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
            }
        }

        [HttpDelete]
        [Route("delete/{req_id}")]
        public async void DeleteUser(int req_id)
        {
            var user = _context.users.First(x => x.id == req_id);
            _context.users.Remove(user);
            _context.SaveChanges();
        }

    }
}
