using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class BlogPost
    {
        public BlogPost()
        {
            GroupedItem = new HashSet<GroupedItem>();
            TopicItem = new HashSet<TopicItem>();
        }

        public int BlogPostId { get; set; }
        public int? BlogInfoId { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogText { get; set; }
        public DateTime PostCreateDate { get; set; }

        public BlogInfo BlogInfo { get; set; }
        public ICollection<GroupedItem> GroupedItem { get; set; }
        public ICollection<TopicItem> TopicItem { get; set; }
    }
}
