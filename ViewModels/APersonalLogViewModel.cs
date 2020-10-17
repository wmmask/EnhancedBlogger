using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnhancedBlogger.ViewModels;
using EnhancedBlogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace EnhancedBlogger.ViewModels
{
    public class APersonalLogViewModel
    {
        public BlogTheme logTheme;  
        public BlogSubTheme logSubThemes;
        public BlogInfo logInfo;
        public BlogPost logPost;
        public IEnumerable<BlogTheme> logListTheme;
        public IEnumerable<BlogSubTheme> logListSubTheme;
        public IEnumerable<BlogInfo> logListInfo;
        public IEnumerable<BlogPost> logListPost;

        public NoteTopic logNote;
        public IEnumerable<NoteTopic> logListNote;


        public LogGrouping logGroups;
        public GroupedItem itemsInGroup;
        public IEnumerable<LogGrouping> logListGroups;  
        public IEnumerable<GroupedItem> itemsListInGroup;

        public APersonalLogViewModel()
        {}
    }
}
