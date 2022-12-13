using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_Parser
{
    public class VK_json_friends
    {

        public class Rootobject
        {
            public Response response { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public Item[] items { get; set; }
        }

        public class Item
        {
            public City city { get; set; }
            public int id { get; set; }
            public string domain { get; set; }
            public string track_code { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public int sex { get; set; }
            public bool can_access_closed { get; set; }
            public bool is_closed { get; set; }
            public string deactivated { get; set; }
        }

        public class City
        {
            public int id { get; set; }
            public string title { get; set; }
        }

    }
}
