using EnhancedBlogger.Models;
using EnhancedBlogger.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnhancedBlogger.ViewModels
{
    public class AThemesWithSubThemesViewModel
    {
        public BlogTheme blogTheme;  //so I have a theme
        public IEnumerable<BlogSubTheme> blogSubThemes;  //and a list of subthemes to go with it
        
        public AThemesWithSubThemesViewModel()
        {
        }



    }
}
