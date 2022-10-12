namespace MojeMiasto.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int city_id { get; set; }
        public int region_id { get; set; }
        public int points { get; set; }

    }
}
