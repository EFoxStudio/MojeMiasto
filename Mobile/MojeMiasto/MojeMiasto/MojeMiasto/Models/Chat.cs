using System.ComponentModel.DataAnnotations;

namespace MojeMiasto.Models
{
    public class Chat
    {
        [Key]
        public int id { get; set; }
        public int user1_id { get; set; }
        public int user2_id { get; set; }
    }
}
