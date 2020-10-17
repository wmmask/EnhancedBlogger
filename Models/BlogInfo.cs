using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class BlogInfo
    {
        public BlogInfo()
        {
            BlogPost = new HashSet<BlogPost>();
        }

        public int BlogInfoId { get; set; }
        public int? BlogSubThemeId { get; set; }
        public string BlogInfoTitle { get; set; }
        public int? UserProfileId { get; set; }
        public DateTime InfoCreateDate { get; set; }

        public BlogSubTheme BlogSubTheme { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<BlogPost> BlogPost { get; set; }
    }
}
