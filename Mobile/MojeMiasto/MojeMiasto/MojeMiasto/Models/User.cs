using EFox.ApiConnection.Toolkit;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MojeMiasto.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int city_id { get; set; }
        public int district_id { get; set; }
        public int points { get; set; }


        public async Task<string> GetProfilePic()
        {
            Connection<string> conn = new Connection<string>();
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");

            return await conn.Get($"users/icon/{ id }");
        }
    }
}
