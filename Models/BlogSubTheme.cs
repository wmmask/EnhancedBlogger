using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class BlogSubTheme
    {
        public BlogSubTheme()
        {
            BlogInfo = new HashSet<BlogInfo>();
        }

        public int BlogSubThemeId { get; set; }
        public int? BlogThemeId { get; set; }
        public string BlogSubThemetitle { get; set; }
        public DateTime SubThemeCreateDate { get; set; }

        public BlogTheme BlogTheme { get; set; }
        public ICollection<BlogInfo> BlogInfo { get; set; }
    }
}
