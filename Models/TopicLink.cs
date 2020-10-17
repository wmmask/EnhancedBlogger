using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class TopicLink
    {
        public int TopicLinkId { get; set; }
        public int? TopicItemId { get; set; }
        public string TopicLinkRemark { get; set; }
        public DateTime LinkCreateDate { get; set; }

        public TopicItem TopicItem { get; set; }
    }
}
