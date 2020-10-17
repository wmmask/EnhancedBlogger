using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class TopicKeyword
    {
        public int TopicKeywordId { get; set; }
        public int? TopicItemId { get; set; }
        public string TopicKeywordName { get; set; }
        public string TopicKeywordRemark { get; set; }
        public DateTime KeywordCreateDate { get; set; }

        public TopicItem TopicItem { get; set; }
    }
}
