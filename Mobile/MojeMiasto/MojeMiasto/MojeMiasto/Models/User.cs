using System.ComponentModel.DataAnnotations;

//Structure of the base User

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

    }
}
