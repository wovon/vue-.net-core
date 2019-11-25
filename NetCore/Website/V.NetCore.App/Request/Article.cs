using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Request
{
    public class Article
    {
        public string author { get; set; }
        public DateTime display_time { get; set; }
        public double forecast { get; set; }
        public int id { get; set; }
        public int importance { get; set; }
        public string pageviews { get; set; }
        public string reviewer { get; set; }
        public string status { get; set; }
        public long timestamp { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }
}
