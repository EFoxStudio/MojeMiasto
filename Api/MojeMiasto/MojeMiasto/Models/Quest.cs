using System.ComponentModel.DataAnnotations;

namespace MojeMiasto.Models
{
    public class Quest
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public int city_id { get; set; }
        public int district_id { get; set; }
        public DateTime create_date { get; set; }
        public DateTime end_date { get; set; }
        public int hired_id { get; set; }
        public bool done { get; set; }
    }
}
