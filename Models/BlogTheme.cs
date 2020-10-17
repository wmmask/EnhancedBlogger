using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class BlogTheme
    {
        public BlogTheme()
        {
            BlogSubTheme = new HashSet<BlogSubTheme>();
        }

        public int BlogThemeId { get; set; }
        public string BlogThemetitle { get; set; }
        public DateTime ThemeCreateDate { get; set; }

        public ICollection<BlogSubTheme> BlogSubTheme { get; set; }
    }
}
