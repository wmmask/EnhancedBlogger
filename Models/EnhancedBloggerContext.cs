using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnhancedBlogger.Models
{
    public partial class EnhancedBloggerContext : DbContext
    {
        public EnhancedBloggerContext()
        {
        }

        public EnhancedBloggerContext(DbContextOptions<EnhancedBloggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogInfo> BlogInfo { get; set; }
        public virtual DbSet<BlogPost> BlogPost { get; set; }
        public virtual DbSet<BlogSubTheme> BlogSubTheme { get; set; }
        public virtual DbSet<BlogTheme> BlogTheme { get; set; }
        public virtual DbSet<GroupedItem> GroupedItem { get; set; }
        public virtual DbSet<LogGrouping> LogGrouping { get; set; }
        public virtual DbSet<NoteTopic> NoteTopic { get; set; }
        public virtual DbSet<TopicItem> TopicItem { get; set; }
        public virtual DbSet<TopicKeyword> TopicKeyword { get; set; }
        public virtual DbSet<TopicLink> TopicLink { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PCOne\\SQLETAP;Database=EnhancedBlogger;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogInfo>(entity =>
            {
                entity.Property(e => e.BlogInfoId).HasColumnName("blogInfoID");

                entity.Property(e => e.BlogInfoTitle)
                    .HasColumnName("blogInfoTitle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BlogSubThemeId).HasColumnName("blogSubThemeId");

                entity.Property(e => e.InfoCreateDate)
                    .HasColumnName("infoCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserProfileId).HasColumnName("userProfileId");

                entity.HasOne(d => d.BlogSubTheme)
                    .WithMany(p => p.BlogInfo)
                    .HasForeignKey(d => d.BlogSubThemeId)
                    .HasConstraintName("FK__BlogInfo__blogSu__2F10007B");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.BlogInfo)
                    .HasForeignKey(d => d.UserProfileId)
                    .HasConstraintName("FK__BlogInfo__userPr__300424B4");
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.Property(e => e.BlogPostId).HasColumnName("blogPostID");

                entity.Property(e => e.BlogInfoId).HasColumnName("blogInfoId");

                entity.Property(e => e.BlogPostTitle)
                    .HasColumnName("blogPostTitle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BlogText)
                    .IsRequired()
                    .HasColumnName("blogText")
                    .HasColumnType("text");

                entity.Property(e => e.PostCreateDate)
                    .HasColumnName("postCreateDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.BlogInfo)
                    .WithMany(p => p.BlogPost)
                    .HasForeignKey(d => d.BlogInfoId)
                    .HasConstraintName("FK__BlogPost__blogIn__32E0915F");
            });

            modelBuilder.Entity<BlogSubTheme>(entity =>
            {
                entity.Property(e => e.BlogSubThemeId).HasColumnName("blogSubThemeID");

                entity.Property(e => e.BlogSubThemetitle)
                    .IsRequired()
                    .HasColumnName("blogSubThemetitle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BlogThemeId).HasColumnName("blogThemeId");

                entity.Property(e => e.SubThemeCreateDate)
                    .HasColumnName("subThemeCreateDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.BlogTheme)
                    .WithMany(p => p.BlogSubTheme)
                    .HasForeignKey(d => d.BlogThemeId)
                    .HasConstraintName("FK__BlogSubTh__blogT__25869641");
            });

            modelBuilder.Entity<BlogTheme>(entity =>
            {
                entity.Property(e => e.BlogThemeId).HasColumnName("blogThemeID");

                entity.Property(e => e.BlogThemetitle)
                    .IsRequired()
                    .HasColumnName("blogThemetitle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ThemeCreateDate)
                    .HasColumnName("themeCreateDate")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<GroupedItem>(entity =>
            {
                entity.Property(e => e.GroupedItemId).HasColumnName("groupedItemID");

                entity.Property(e => e.BlogPostId).HasColumnName("blogPostId");

                entity.Property(e => e.GroupedItemComment)
                    .IsRequired()
                    .HasColumnName("groupedItemComment")
                    .HasColumnType("text");

                entity.Property(e => e.ItemCreateDate)
                    .HasColumnName("itemCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.LogGroupingId).HasColumnName("logGroupingId");

                entity.HasOne(d => d.BlogPost)
                    .WithMany(p => p.GroupedItem)
                    .HasForeignKey(d => d.BlogPostId)
                    .HasConstraintName("FK__GroupedIt__blogP__398D8EEE");

                entity.HasOne(d => d.LogGrouping)
                    .WithMany(p => p.GroupedItem)
                    .HasForeignKey(d => d.LogGroupingId)
                    .HasConstraintName("FK__GroupedIt__logGr__38996AB5");
            });

            modelBuilder.Entity<LogGrouping>(entity =>
            {
                entity.Property(e => e.LogGroupingId).HasColumnName("logGroupingID");

                entity.Property(e => e.GroupingCreateDate).HasColumnType("datetime");

                entity.Property(e => e.LogGroupingComment)
                    .IsRequired()
                    .HasColumnName("logGroupingComment")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogGroupingtitle)
                    .IsRequired()
                    .HasColumnName("logGroupingtitle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserProfileId).HasColumnName("userProfileId");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.LogGrouping)
                    .HasForeignKey(d => d.UserProfileId)
                    .HasConstraintName("FK__LogGroupi__userP__35BCFE0A");
            });

            modelBuilder.Entity<NoteTopic>(entity =>
            {
                entity.Property(e => e.NoteTopicId).HasColumnName("noteTopicID");

                entity.Property(e => e.NoteTopicComment)
                    .IsRequired()
                    .HasColumnName("noteTopicComment")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoteTopicTitle)
                    .IsRequired()
                    .HasColumnName("noteTopicTitle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TopicCreateDate)
                    .HasColumnName("topicCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserProfileId).HasColumnName("userProfileId");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.NoteTopic)
                    .HasForeignKey(d => d.UserProfileId)
                    .HasConstraintName("FK__NoteTopic__userP__3C69FB99");
            });

            modelBuilder.Entity<TopicItem>(entity =>
            {
                entity.Property(e => e.TopicItemId).HasColumnName("topicItemID");

                entity.Property(e => e.BlogPostId).HasColumnName("blogPostId");

                entity.Property(e => e.ItemCreateDate)
                    .HasColumnName("itemCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.NoteTopicId).HasColumnName("noteTopicId");

                entity.Property(e => e.TopicItemThought)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BlogPost)
                    .WithMany(p => p.TopicItem)
                    .HasForeignKey(d => d.BlogPostId)
                    .HasConstraintName("FK__TopicItem__blogP__403A8C7D");

                entity.HasOne(d => d.NoteTopic)
                    .WithMany(p => p.TopicItem)
                    .HasForeignKey(d => d.NoteTopicId)
                    .HasConstraintName("FK__TopicItem__noteT__3F466844");
            });

            modelBuilder.Entity<TopicKeyword>(entity =>
            {
                entity.Property(e => e.TopicKeywordId).HasColumnName("topicKeywordID");

                entity.Property(e => e.KeywordCreateDate)
                    .HasColumnName("keywordCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TopicItemId).HasColumnName("topicItemId");

                entity.Property(e => e.TopicKeywordName)
                    .IsRequired()
                    .HasColumnName("topicKeywordName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TopicKeywordRemark)
                    .HasColumnName("topicKeywordRemark")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.TopicItem)
                    .WithMany(p => p.TopicKeyword)
                    .HasForeignKey(d => d.TopicItemId)
                    .HasConstraintName("FK__TopicKeyw__topic__45F365D3");
            });

            modelBuilder.Entity<TopicLink>(entity =>
            {
                entity.ToTable("topicLink");

                entity.Property(e => e.TopicLinkId).HasColumnName("topicLinkID");

                entity.Property(e => e.LinkCreateDate)
                    .HasColumnName("linkCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TopicItemId).HasColumnName("topicItemId");

                entity.Property(e => e.TopicLinkRemark)
                    .HasColumnName("topicLinkRemark")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.TopicItem)
                    .WithMany(p => p.TopicLink)
                    .HasForeignKey(d => d.TopicItemId)
                    .HasConstraintName("FK__topicLink__topic__4316F928");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.UserProfileId).HasColumnName("userProfileID");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasColumnName("bio")
                    .HasColumnType("text");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasColumnName("nickName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileCreateDate)
                    .HasColumnName("profileCreateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserProfileAccount)
                    .IsRequired()
                    .HasColumnName("userProfileAccount")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
