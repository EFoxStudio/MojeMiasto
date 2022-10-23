using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


//Structure of the base UI_Quest

namespace MojeMiasto.Models
{
    public class UI_Quest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public UI_User user { get; set; }
        public int city_id { get; set; }
        public string city { get; set; }
        public int district_id { get; set; }
        public string district { get; set; }
        public DateTime create_date { get; set; }
        public DateTime end_date { get; set; }
        public bool isHired { get; set; }
        public int hired_id { get; set; }
        public UI_User hired { get; set; }
        public bool done { get; set; }
        public string location { get; set; }
    }
}
