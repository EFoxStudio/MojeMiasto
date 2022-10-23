using System.ComponentModel.DataAnnotations;

//Structure of the base City

namespace MojeMiasto.Models
{
    public class City
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
