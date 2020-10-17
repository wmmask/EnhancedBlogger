using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class TopicItem
    {
        public TopicItem()
        {
            TopicKeyword = new HashSet<TopicKeyword>();
            TopicLink = new HashSet<TopicLink>();
        }

        public int TopicItemId { get; set; }
        public int? NoteTopicId { get; set; }
        public int? BlogPostId { get; set; }
        public string TopicItemThought { get; set; }
        public DateTime ItemCreateDate { get; set; }

        public BlogPost BlogPost { get; set; }
        public NoteTopic NoteTopic { get; set; }
        public ICollection<TopicKeyword> TopicKeyword { get; set; }
        public ICollection<TopicLink> TopicLink { get; set; }
    }
}
