using System;
using System.ComponentModel.DataAnnotations;

namespace MojeMiasto.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        public string text { get; set; }
        public int chat_id { get; set; }
        public DateTime date { get; set; }
    }
}
