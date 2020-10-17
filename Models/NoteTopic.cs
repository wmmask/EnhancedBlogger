using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class NoteTopic
    {
        public NoteTopic()
        {
            TopicItem = new HashSet<TopicItem>();
        }

        public int NoteTopicId { get; set; }
        public int? UserProfileId { get; set; }
        public string NoteTopicTitle { get; set; }
        public string NoteTopicComment { get; set; }
        public DateTime TopicCreateDate { get; set; }

        public UserProfile UserProfile { get; set; }
        public ICollection<TopicItem> TopicItem { get; set; }
    }
}
