using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class LogGrouping
    {
        public LogGrouping()
        {
            GroupedItem = new HashSet<GroupedItem>();
        }

        public int LogGroupingId { get; set; }
        public int? UserProfileId { get; set; }
        public string LogGroupingtitle { get; set; }
        public string LogGroupingComment { get; set; }
        public DateTime GroupingCreateDate { get; set; }

        public UserProfile UserProfile { get; set; }
        public ICollection<GroupedItem> GroupedItem { get; set; }
    }
}
