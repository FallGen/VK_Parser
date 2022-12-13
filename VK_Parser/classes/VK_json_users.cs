using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_Parser
{
    public class VK_json_users
    {

        public class Rootobject
        {
            public Response[] response { get; set; }
        }

        public class Response
        {
            public int id { get; set; }
            public string domain { get; set; }
            public City city { get; set; }
            public string photo_max_orig { get; set; }
            public string site { get; set; }
            public string status { get; set; }
            public Last_Seen last_seen { get; set; }
            public int university { get; set; }
            public string university_name { get; set; }
            public int faculty { get; set; }
            public string faculty_name { get; set; }
            public int graduation { get; set; }
            public string education_form { get; set; }
            public Relation_Partner relation_partner { get; set; }
            public string education_status { get; set; }
            public string home_town { get; set; }
            public int relation { get; set; }
            public Counters counters { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public bool can_access_closed { get; set; }
            public bool is_closed { get; set; }
            public string bdate { get; set; }
            public string home_phone { get; set; }
            public string mobile_phone { get; set; }
            public string posts { get; set; }
            public string name { get; set; }
            public int sex { get; set; }

        }

        public class City
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Last_Seen
        {
            public int platform { get; set; }
            public int time { get; set; }
        }

        public class Counters
        {
            public int albums { get; set; }
            public int audios { get; set; }
            public int followers { get; set; }
            public int friends { get; set; }
            public int gifts { get; set; }
            public int pages { get; set; }
            public int notes { get; set; }
            public int photos { get; set; }
            public int subscriptions { get; set; }
            public int videos { get; set; }
            public int clips_followers { get; set; }
        }

        public class Relation_Partner
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
        }



    }

}
