using NewBlogAPI.Components.BlogArticleComponents;
using Microsoft.AspNet.Identity.EntityFramework;
using NewBlogAPI.Components.AdminComponents;
using System.Data.Entity;
using NewBlogAPI.Components.BlogArticleComponents.Services;
using NewBlogAPI.Models;

#if !FILES_TO_FILESYSTEM
using NewBlogAPI.Components.BlogArticleFilesComponents;
#endif

namespace NewBlogAPI.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BlogDBContext: IdentityDbContext<BlogAdmin>
    {
        public BlogDBContext() : base("MyDbContextConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static BlogDBContext Create()
        {
            return new BlogDBContext();
        }

#if !FILES_TO_FILESYSTEM
        public DbSet<BlogArticleFile> BlogArticleFiles { get; set; }
#endif

        public DbSet<BlogArticle> BlogArticles { get; set; }
        public DbSet<BlogArticleTag> BlogArticlesTags { get; set; }
        public DbSet<BlogArticleComment> BlogArticleComments { get; set; }
        public DbSet<BlogAdminTag> BlogAdminsTags { get; set; }

      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.
                Entity<BlogAdminTag>().
                HasKey( entitie => new
                {
                    entitie.AdminId,
                    entitie.TagValue
                });

            modelBuilder.
                Entity<BlogArticleTag>().
                HasKey(entitie => new
                {
                    entitie.BlogArticleId,
                    entitie.TagValue
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}