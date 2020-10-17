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
        public BlogTheme themeWork;
        public BlogSubTheme subThemesWork;
        public BlogInfo infoWork;
        public BlogPost postWork;
        public IEnumerable<BlogTheme> themeWorkList;
        public IEnumerable<BlogSubTheme> subThemeWorkList;
        public IEnumerable<BlogInfo> infoworkList;
        public IEnumerable<BlogPost> postWorkList;

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
