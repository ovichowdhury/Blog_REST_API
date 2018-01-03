using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_REST_API.Models
{
    public class ArticleGist
    {
        public int id { get; set; }
        public string subject { get; set; }
        public DateTime date { get; set; }
        public string url { get; set; }
    }
}