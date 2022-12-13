using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_Parser
{
    internal class VK_json_photos
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
            public int album_id { get; set; }
            public int date { get; set; }
            public int id { get; set; }
            public int owner_id { get; set; }
            public int can_comment { get; set; }
            public int post_id { get; set; }
            public Size[] sizes { get; set; }
            public string text { get; set; }
            public bool has_tags { get; set; }
            public Likes likes { get; set; }
            public Comments comments { get; set; }
            public Reposts reposts { get; set; }
            public Tags tags { get; set; }
        }

        public class Likes
        {
            public int count { get; set; }
            public int user_likes { get; set; }
        }

        public class Comments
        {
            public int count { get; set; }
        }

        public class Reposts
        {
            public int count { get; set; }
        }

        public class Tags
        {
            public int count { get; set; }
        }

        public class Size
        {
            public int height { get; set; }
            public string url { get; set; }
            public string type { get; set; }
            public int width { get; set; }
        }

    }
}
