using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            BlogInfo = new HashSet<BlogInfo>();
            LogGrouping = new HashSet<LogGrouping>();
            NoteTopic = new HashSet<NoteTopic>();
        }

        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int? Age { get; set; }
        public string Bio { get; set; }
        public DateTime ProfileCreateDate { get; set; }
        public string UserProfileAccount { get; set; }

        public ICollection<BlogInfo> BlogInfo { get; set; }
        public ICollection<LogGrouping> LogGrouping { get; set; }
        public ICollection<NoteTopic> NoteTopic { get; set; }
    }
}
