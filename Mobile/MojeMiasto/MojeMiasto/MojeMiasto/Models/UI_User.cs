using System;
using System.Collections.Generic;
using System.Text;

//Structure of the base UI_User

namespace MojeMiasto.Models
{
    public class UI_User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int city_id { get; set; }
        public string city { get; set; }
        public int district_id { get; set; }
        public string district { get; set; }
        public int points { get; set; }
        public string iconUrl { get; set; }
        public string location { get; set; }
        public List<Quest> quests { get; set; }
    }
}
