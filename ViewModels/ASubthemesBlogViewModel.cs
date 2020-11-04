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
    public class ASubthemesBlogViewModel
    {
        //A cookie of sorts
        public int guide;
        
        //Bringing in models related to: WorkSpace Summary
        public NoteTopic thisNote;
        public IEnumerable<NoteTopic> thisNoteList;

        //Bringing in models related to: Personal Log Summary
        public LogGrouping thisGroup;
        public IEnumerable<LogGrouping> thisGroupList;

        //Bringing in models related to: Prpfile Summary
        public UserProfile thisUser;
        public IEnumerable<UserProfile> thisUserList;

        //Bringing in models related to: Themes, Subthemes, and Blogs
        public BlogTheme pageTheme;  //so I have a theme
        public BlogSubTheme pageSubTheme;  //and a subthemes to go with it
        public BlogInfo pageInfo;  //so I have a theme
        public BlogPost thisBlog;  //and a subthemes to go with it
        public IEnumerable<BlogTheme> thisThemeList;  //and a list of blog header for each subthemes to go with it
        public IEnumerable<BlogSubTheme> thisSubThemeList; //and a list of blog Posts to go with subthemes
        public IEnumerable<BlogInfo> pageInfoList;  //and a list of blog header for each subthemes to go with it
        public IEnumerable<BlogPost> pageBlogList; //and a list of blog Posts to go with subthemes

        public ASubthemesBlogViewModel()
        {

        }
    }
}
