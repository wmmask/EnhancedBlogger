using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnhancedBlogger.Models;
using EnhancedBlogger.ViewModels;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore;
using Remotion.Linq.Clauses;

namespace EnhancedBlogger.Controllers
{
    public class NoteTopicsController : Controller
    {
        private readonly EnhancedBloggerContext _context;
        
        public NoteTopicsController(EnhancedBloggerContext context)
        {
            _context = context;
        }
        // GET: NoteTopics
        public IActionResult MyWorkSpace()
        {
            AWorkSpaceViewModel workSpace = new AWorkSpaceViewModel
            {
                //personalLog.blogSubThemes = _context.BlogSubTheme.Where(s => s.BlogThemeId == id);
                myTopicsList = _context.NoteTopic,//.Where(t => t.NoteTopicId < 12),
                myItemsList = _context.TopicItem,//.Where(t => t.TopicItemId < 12),
                myKeywordList = _context.TopicKeyword,//.Where(t => t.TopicKeywordId < 12),
                myLinkList = _context.TopicLink,//.Where(t => t.TopicLinkId < 12),
                logListGroups = _context.LogGrouping//.Where(t => t.LogGroupingId < 12)
            };

            return View(workSpace);
        }

        // GET: NoteTopics
        public IActionResult  MyPersonalLog()
        {
            APersonalLogViewModel personalLog = new APersonalLogViewModel
            {
                //personalLog.blogSubThemes = _context.BlogSubTheme.Where(s => s.BlogThemeId == id);
                 logListGroups = _context.LogGrouping.Where(t => t.LogGroupingId < 12),
                //int thisID = personalLog.logListGroups.
                itemsListInGroup = _context.GroupedItem.Where(t => t.LogGroupingId < 12),
                logListTopic = _context.NoteTopic.Where(t => t.NoteTopicId < 12)
            };
            return View(personalLog);
        }
        
        public IActionResult SubthemeYpage(int? id) // calling in BlogSubThemeId
        {
            ASubthemesBlogViewModel mySubsPosts = new ASubthemesBlogViewModel();

            //*******************Assign theme and Subtheme from input agurment
            mySubsPosts.pageSubTheme = _context.BlogSubTheme.FirstOrDefault(s => s.BlogSubThemeId == id);

            var queryFortheme = _context.BlogSubTheme.FirstOrDefault(o => o.BlogSubThemeId == id);
            var themeId = queryFortheme.BlogThemeId;
            
            //so now I have to assign to this model the record of BlogTheme corresponding to the BlogThemeId.
           mySubsPosts.pageTheme = _context.BlogTheme.FirstOrDefault(bt => bt.BlogThemeId ==  themeId);


        //*******************Assign the Posts (BlogPost) and Headers (BlogInfo) associated with the subtheme
          //I. to get the blog info records:
            //A. Make a list of and assign all BlogInfo records associated w/ the subtheme
         //II. Get the blog post records
            //A. Make a list of and assign all BlogInfo records associated w/ the subtheme--SAME AS I.A!!
            //B. Make a list of all BlogPost records
            //C. I then have to look through my list of posts and pull out the various BlogInfoId of interest
            //D. And then assign those blog posts to my instance.
            //++++++Implement Infos and Posts:
            //A.
            //assign all Info Headings assocaited with the subTheme
            mySubsPosts.pageInfoList = _context.BlogInfo.Where(s => s.BlogSubThemeId == id);
            //B.
            //
            var postList = _context.BlogPost;
            //C.
            // Now list of posts and pull out the various BlogInfoId of interest

            //D.
            //For now just go with all blogs
            
            mySubsPosts.pageBlogList = _context.BlogPost;
            
            /*
            var infoList = mySubsPosts.pageInfoList;
            int grp = mySubsPosts.pageInfoList.BlogInfoTd;
            var myPosts = from c in postList select c.BlogInfoId;
            mySubsPosts.pageBlogList = postList.Where(c => c.BlogInfoId == _context.BlogInfo);
            */
            //go through the list of posts and query fro relevant infoids
            //======mySubsPosts.pageBlogList = _context.BlogPost.Where(c => c.BlogInfoId == );
            //now i need to get the column value called BlogInfoID for all records associated w/ the subtheme
            
            //var queryForInfo = _context.BlogInfo.Where(i => i.BlogSubThemeId == id);
            //so now i have a list of BlogInfo records haveing thier column, BlogSubThemeId= my chosen subtheme.
            //thus must now extract all the BlogInfoIDs for the BlogInfo lidt of records
            
            //var myPosts = from c in queryForInfo select c.BlogInfoId;
            // and now i have to find away to link all blog posts who bloginfoid matches the list from BlogInfo
            //query all blog posts associated with the blog info column, bloginfoid.
            //int postIds = myPosts.BlogInfoId;
            //var queryForPosts = _context.BlogPost.Where(i => i.BlogInfoId == myPosts);
            //assign to the model the list of blog posts associated w/ the Info Hedings.
            //mySubsPosts.pageBlogList = _context.BlogPost.Where(c => c.BlogInfoId == queryForInfo);
            //BlogPost.Contains(postId);


            //.Contains()Where(s => s.BlogPostId == _context.BlogI);
        //******************************8ADD the PL  and  the WS  Summary
            mySubsPosts.thisGroupList = _context.LogGrouping;//.Where(s => s.LogGroupingId < td);
            
            mySubsPosts.thisNoteList = _context.NoteTopic;//.Where(s => s.NoteTopicId < td);

            //int? cook = id;
            //mySubsPosts.guide = (int)cook;
            return View(mySubsPosts);
        }
        public IActionResult ThemeXpage(int id)//calling BlogThemeId
        {
            ASubthemesBlogsViewModel myThemesSubs = new ASubthemesBlogsViewModel
            {
                thisTheme = _context.BlogTheme.FirstOrDefault(t => t.BlogThemeId == id),
                thisSubThemeList = _context.BlogSubTheme.Where(s => s.BlogThemeId == id)
            };

            return View(myThemesSubs);
        }

        public IActionResult PickTheme()
        {
            ASubthemesBlogsViewModel myThemes = new ASubthemesBlogsViewModel
            {
                thisThemeList = _context.BlogTheme//.Where(t => t.NoteTopicId < 12);
            };
            return View(myThemes);
        }


        // GET: NoteTopics
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.NoteTopic.Include(n => n.UserProfile);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: NoteTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTopic = await _context.NoteTopic
                .Include(n => n.UserProfile)
                .FirstOrDefaultAsync(m => m.NoteTopicId == id);
            if (noteTopic == null)
            {
                return NotFound();
            }

            return View(noteTopic);
        }

        /// ////// ADDS
        /// ******* Add Item
                // GET: GroupedItems/Add Item
        public IActionResult AddItem(int? id)
        {
            /*
                       var enhancedBloggerContext = _context.NoteTopic.Include(n => n.UserProfile);
                        //Make a list of the existing LogGroupings:
                        List<LogGrouping> extgGroups = new List<LogGrouping>();
                        foreach(var log in extgGroups)
                        {
                            @Html.DisplayFor(m => log.LogGroupingId)
                            @Html.DisplayFor(m => log.LogGroupingtitle)
                            @Html.DisplayFor(m => log.LogGroupingComment)
                        };
              */
            GroupedItem GItem = new GroupedItem();
            //var thisblogsID = id;
            ViewBag.my2ID = id;


            return View(GItem); //we will return the view named CreateItem
        }

        // POST: GroupedItems/Add Item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem([Bind("LogGroupingId,BlogPostId," +
            "GroupedItemComment,ItemCreateDate")] GroupedItem groupedItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupedItem);//wait scaffoled controllers Add to the table,not the dB
                _context.SaveChanges();
                return RedirectToAction(nameof(MyPersonalLog)); //so now we tell the app where to return to!
            }
            return View(groupedItem);
        }


         // GET: TopicItems/Add TopicItem
        public IActionResult AddTopicItem(int? id)
        {
            TopicItem TItem = new TopicItem();
            //var thisblogsID = id;
            ViewBag.myID = id;
            
            //var thisAddItem = _context.TopicItem.Include(n => n.BlogPost);

            return View(TItem);
            
        }

        // POST: TopicItems/CreateTopicItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTopicItem([Bind("NoteTopicId,BlogPostId,TopicItemThought," +
            "ItemCreateDate")] TopicItem topicItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicItem);
        }

        // GET: TopicKeywords/Add Keyword
        public IActionResult AddKeyword()
        {
            TopicKeyword myKey = new TopicKeyword();
            return View(myKey);
        }

        // POST: TopicKeywords/CreateKeyword
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddKeyword([Bind("TopicItemId,TopicKeywordName,TopicKeywordRemark," +
            "KeywordCreateDate")] TopicKeyword topicKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicKeyword);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicKeyword);
        }

        // GET: TopicLinks/Add Link   
        public IActionResult AddLink()
        {
            TopicLink myLink = new TopicLink();
            return View(myLink);
        }

        // POST: TopicLinks/Add Links
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLink([Bind("TopicItemId,TopicLinkRemark,LinkCreateDate")] TopicLink topicLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicLink);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicLink);
        }


        //*****************CREATES
        // GET: NoteTopics/Create
        public IActionResult Create()
        {
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio");
            return View();
        }

        // POST: NoteTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteTopicId,UserProfileId," +
            "NoteTopicTitle,NoteTopicComment,TopicCreateDate")] NoteTopic noteTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", noteTopic.UserProfileId);
            return View(noteTopic);
        }

        // GET: LogGroupings/CreateGroup
        public IActionResult CreateGroup()
        {
            //LogGroupingEditViewModel myEdit = new LogGroupingEditViewModel();
           

            return View(); //we will return the view named CreateGroup
        }

        // POST: LogGroupings/CreateGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGroup([Bind("UserProfileId,LogGroupingtitle," +
            "LogGroupingComment,GroupingCreateDate")] LogGrouping logGrouping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logGrouping);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyPersonalLog)); //so now we tell the app where to return to!
            }
            return View(logGrouping);
        }

        // GET: GroupedItems/CreateItem
        public IActionResult CreateItem()
        {
            GroupedItem myItem =  new GroupedItem();
            return View(myItem); //we will return the view named CreateItem
        }

        // POST: GroupedItems/CreateItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem([Bind("LogGroupingId,BlogPostId," +
            "GroupedItemComment,ItemCreateDate")] GroupedItem groupedItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupedItem);//wait scaffoled controllers Add to the table,not the dB
                _context.SaveChanges();
                return RedirectToAction(nameof(MyPersonalLog)); //so now we tell the app where to return to!
            }
            return View(groupedItem);
        }

        // GET: NoteTopics/CreateYopic
        public IActionResult CreateTopic()
        {
            NoteTopic myNote = new NoteTopic();
            return View(myNote);
        }

        // POST: NoteTopics/CreateTopic
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTopic([Bind("UserProfileId," +
            "NoteTopicTitle,NoteTopicComment,TopicCreateDate")] NoteTopic noteTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteTopic);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(noteTopic);
        }

        // GET: TopicItems/CreateTopicItem
        public IActionResult CreateTopicItem()
        {
            TopicItem TItem = new TopicItem();
            return View(TItem);
        }

        // POST: TopicItems/CreateTopicItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTopicItem([Bind("NoteTopicId,BlogPostId,TopicItemThought," +
            "ItemCreateDate")] TopicItem topicItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicItem);
        }

        // GET: TopicKeywords/CreateKeyword
        public IActionResult CreateKeyword()
        {
            TopicKeyword myKey = new TopicKeyword();
            return View(myKey);
        }

        // POST: TopicKeywords/CreateKeyword
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKeyword([Bind("TopicItemId,TopicKeywordName,TopicKeywordRemark," +
            "KeywordCreateDate")] TopicKeyword topicKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicKeyword);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicKeyword);
        }

        // GET: TopicLinks/CreateLink   
        public IActionResult CreateLink()
        {
            TopicLink myLink = new TopicLink();
            return View(myLink);
        }

        // POST: TopicLinks/CreateLinks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLink([Bind("TopicItemId,TopicLinkRemark,LinkCreateDate")] TopicLink topicLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicLink);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicLink);
        }

        /// ////////////////Vreate a blog here
        // GET: BlogPosts/CreateBlogPost
        public IActionResult CreateBlogPost(int? id)
        {
            BlogPost blogPost = new BlogPost();
            ViewBag.my2ID = id;

            return View(blogPost);
        }

        // POST: BlogPosts/CreateBlogPost
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBlogPost([Bind("BlogInfoId,BlogPostTitle,BlogText," +
            "PostCreateDate")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                _context.SaveChanges();
                return RedirectToAction(nameof(SubthemeYpage));
            }
            return View(blogPost);
        }

        // POST: NoteTopics/Edit/5
        public IActionResult Edit(int? id)
        {
            TopicItem myTItem = _context.TopicItem.Find(id);
            return View(myTItem);
        }
        // POST: NoteTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id ,[Bind("NoteTopicId,UserProfileId,NoteTopicTitle," +
            "NoteTopicComment,TopicCreateDate")] NoteTopic noteTopic)
        {
            if (id != noteTopic.NoteTopicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    _context.Update(noteTopic);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", noteTopic.UserProfileId);
            return View(noteTopic);
        }

        // GET: NoteTopics/EditGroup
        public IActionResult EditGroup(int? id)
        {
            LogGrouping myGroup = _context.LogGrouping.Find(id);
            return View(myGroup);
        }

        // GET: NoteTopics/EditGroup ---Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGroup([Bind("LogGroupingId,UserProfileId,LogGroupingtitle,LogGroupingComment," +
            "GroupingCreateDate")] LogGrouping logGrouping)
        {
            if (ModelState.IsValid)
            {
                _context.Update(logGrouping);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyPersonalLog));//index sometimes used as equivalent to List() action   
            }
            return View(logGrouping);
        }

        // GET: GroupedItems/EditItem
        public IActionResult EditItem(int? id)
        {
            GroupedItem myItem = _context.GroupedItem.Find(id);
//          //  int? myLog = ViewBag.thisLogged;
            //ViewBag.thisLogged2 = sid;
            return View(myItem);
        }

        // POST: GroupedItems/EditItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem([Bind("GroupedItemId,LogGroupingId,BlogPostId," +
            "GroupedItemComment,ItemCreateDate")] GroupedItem groupedItem)
        {
            if (ModelState.IsValid)
            {
                _context.Update(groupedItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyPersonalLog)); // index sometimes used as  equivalent to List() action   
            }
            return View(groupedItem);
        }

        ///********************Edit Note
        // GET: NoteTopics/EditNote
        public IActionResult EditNote(int? id)
        {
            NoteTopic myTopic = _context.NoteTopic.Find(id);
            return View(myTopic);
        }

        // POST: NoteTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote([Bind("NoteTopicId,UserProfileId,NoteTopicTitle," +
            "NoteTopicComment,TopicCreateDate")] NoteTopic noteTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Update(noteTopic);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(noteTopic);
        }

        ///********************Edit TopicItem
                // GET: TopicItems/EditTopicItem
        public IActionResult EditTopicItem(int? id)
        {
            TopicItem myTItem = _context.TopicItem.Find(id);
            return View(myTItem);
        }

        // POST: TopicItems/EditTopicItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTopicItem([Bind("TopicItemId,NoteTopicId,BlogPostId," +
            "TopicItemThought,ItemCreateDate")] TopicItem topicItem)
        {
            if (ModelState.IsValid)
            {
                _context.Update(topicItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }

            return View(topicItem);
        }

        ///********************Edit Keyword
                // GET: TopicKeywords/Edit/5
        // GET: TopicKeywords/Edit/5
        public IActionResult EditKeyword(int? id)
        {
            TopicKeyword myKeyw = _context.TopicKeyword.Find(id);
            return View(myKeyw);
        }

        // POST: TopicKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditKeyword([Bind("TopicKeywordId,TopicItemId," +
            "TopicKeywordName,TopicKeywordRemark,KeywordCreateDate")] TopicKeyword topicKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Update(topicKeyword);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicKeyword);
        }

        ///********************Edit Link
        // GET: TopicLinks/Edit/5
        public IActionResult EditLink(int? id)
        {
            TopicLink myLink = _context.TopicLink.Find(id);
            //so now retrieve the topicitemid
            
            return View(myLink);
        }

        // POST: TopicLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLink([Bind("TopicLinkId,TopicItemId,TopicLinkRemark," +
            "LinkCreateDate")] TopicLink topicLink)
        {
            if (ModelState.IsValid)
            {
                _context.Update(topicLink);
                _context.SaveChanges();
                return RedirectToAction(nameof(MyWorkSpace));
            }
            return View(topicLink);
        }

        // GET: NoteTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTopic = await _context.NoteTopic
                .Include(n => n.UserProfile)
                .FirstOrDefaultAsync(m => m.NoteTopicId == id);
            if (noteTopic == null)
            {
                return NotFound();
            }

            return View(noteTopic);
        }

        // POST: NoteTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteTopic = await _context.NoteTopic.FindAsync(id);
            _context.NoteTopic.Remove(noteTopic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteTopicExists(int id)
        {
            return _context.NoteTopic.Any(e => e.NoteTopicId == id);
        }
    }
}
