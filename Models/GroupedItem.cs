using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class GroupedItem
    {
        public int GroupedItemId { get; set; }
        public int? LogGroupingId { get; set; }
        public int? BlogPostId { get; set; }
        public string GroupedItemComment { get; set; }
        public DateTime ItemCreateDate { get; set; }

        public BlogPost BlogPost { get; set; }
        public LogGrouping LogGrouping { get; set; }
    }
}
