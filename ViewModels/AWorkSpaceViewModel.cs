using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnhancedBlogger.ViewModels;
using EnhancedBlogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using EnhancedBlogger.Controllers;

namespace EnhancedBlogger.ViewModels
{
    public class AWorkSpaceViewModel
    {
        //Bringing in models related to: Themes, Subthemes, and Blogs
        public BlogTheme themeWork;
        public BlogSubTheme subThemesWork;
        public BlogInfo infoWork;
        public BlogPost postWork;
        public IEnumerable<BlogTheme> themeWorkList;
        public IEnumerable<BlogSubTheme> subThemeWorkList;
        public IEnumerable<BlogInfo> infoworkList;
        public IEnumerable<BlogPost> postWorkList;

        //Bringing in models related to: Personal Log Summary
        public LogGrouping logGroups;
        public IEnumerable<LogGrouping> logListGroups;

        //Bringing in models related to: Prpfile Summary
        public UserProfile up;
        public IEnumerable<UserProfile> pu;

        //Bringing in models related to: Work Space
        public NoteTopic myTopic;
        public TopicItem myItem;
        public TopicKeyword myKeyword;
        public TopicLink myLinks;
        public IEnumerable<NoteTopic> myTopicsList;
        public IEnumerable<TopicItem> myItemsList;
        public IEnumerable<TopicKeyword> myKeywordList;
        public IEnumerable<TopicLink> myLinkList;

        public  AWorkSpaceViewModel()
        {}
    }
}
