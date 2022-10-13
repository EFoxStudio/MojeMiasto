using System.ComponentModel.DataAnnotations;

namespace MojeMiasto.Models
{
    public class City
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
