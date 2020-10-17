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
    public class ASubthemesBlogsViewModel
    {
        public BlogTheme thisTheme;  //so I have a theme
        public BlogSubTheme thisSubThemes;  //and a subthemes to go with it
        public IEnumerable<BlogInfo> blogInfo;  //and a list of blog header for each subthemes to go with it
        public IEnumerable<BlogPost> blogPosts; //and a list of blog Posts to go with subthemes

        public ASubthemesBlogsViewModel()
        {

        }
    }
}
