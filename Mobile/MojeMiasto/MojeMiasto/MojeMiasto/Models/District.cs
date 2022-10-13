using System.ComponentModel.DataAnnotations;

namespace MojeMiasto.Models
{
    public class District
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int city_id { get; set; }
    }
}
