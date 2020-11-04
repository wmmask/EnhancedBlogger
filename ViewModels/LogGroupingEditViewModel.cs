using System;
using System.Collections.Generic;

namespace EnhancedBlogger.Models
{
    public partial class LogGroupingEditViewModel
    {
        public LogGroupingEditViewModel()
        { }

        public LogGrouping myGroup;
        public IEnumerable<LogGrouping> myGroups;

        public int LogGroupingId;
        public int? UserProfileId;
        public string LogGroupingtitle ;
        public string LogGroupingComment ;
        public DateTime GroupingCreateDate ;

        public UserProfile UserProfile;

    }
}
