using NewBlogAPI.Components.BlogArticleComponents.Services.Interfaces;
using NewBlogAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if FILES_TO_FILESYSTEM

    using System.IO;

#endif

namespace NewBlogAPI.Components.BlogArticleComponents.Services
{
    public class BlogArticleService : IBlogArticleService 
    {
        private readonly BlogDBContext _blogDBContext;
        public BlogArticleService(BlogDBContext inBlogDBContext)
        {
            _blogDBContext = inBlogDBContext;
        }

        //***       Articles

        ///<exception cref="SystemException"></exception>
#if FILES_TO_FILESYSTEM
        public async Task<BlogArticle> CreateArticle(BlogArticle inArticle, string inBasePath)
#else
        public async Task<BlogArticle> CreateArticle(BlogArticle inArticle)
#endif
        {
            return await Task.Run(
                () =>
                {
                    inArticle.CreateDate = DateTime.Now;

                    _blogDBContext.BlogArticles.Add(inArticle);

                    _blogDBContext.SaveChanges();

#if FILES_TO_FILESYSTEM

                    Directory.CreateDirectory
                    (
                        inBasePath + $"\\{ inArticle.Id}"
                    );

#endif

                    return inArticle;
                });
        }

        ///<exception cref="ArgumentNullException"></exception>
        public async Task<List<BlogArticle>> GetAllArticles()
        {
            return await Task.Run(
                () => 
                    _blogDBContext.BlogArticles.ToList());
        }

        ///<exception cref="SystemException"></exception>
        public async Task<BlogArticle> GetArticle(long inArticleId)
        {
            return await Task.Run(
                () =>
                    _blogDBContext.
                        BlogArticles.
                        SingleOrDefault(record => record.Id == inArticleId));
        }

        ///<exception cref="SystemException"></exception>
#if FILES_TO_FILESYSTEM
        public async Task<BlogArticle> DeleteArticle(long inArticleId, string inBasePath)
#else
        public async Task<BlogArticle> DeleteArticle(long inArticleId)
#endif
        {
            return await Task.Run(
                    () =>
                    {
                        BlogArticle result = _blogDBContext.
                                                BlogArticles.
                                                Remove(new BlogArticle { Id = inArticleId});

                        _blogDBContext.SaveChanges();

#if FILES_TO_FILESYSTEM

                        Directory.Delete
                        (
                           inBasePath + $"\\{ inArticleId }"
                        );

#endif
                        return result;
                    });
        }

        ///<exception cref="SystemException"></exception>
        public async Task UpdateArticle(BlogArticle inArticle)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.
                    Entry(  _blogDBContext.
                                BlogArticles.
                                SingleOrDefault(record => record.Id == inArticle.Id)).
                    CurrentValues.SetValues(inArticle);

                    _blogDBContext.SaveChanges();
                });
        }

        //***       Tags

        ///<exception cref="SystemException"></exception>
        public async Task AddTag(BlogArticleTag inTag)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.BlogArticlesTags.Add(inTag);
                    _blogDBContext.SaveChanges();
                });
        }

        ///<exception cref=SystemException"></exception>
        public async Task DeleteTag(BlogArticleTag inTag)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.
                        BlogArticlesTags.
                        Remove( _blogDBContext.
                                    BlogArticlesTags.
                                    Attach(inTag));

                    _blogDBContext.SaveChanges();
                });
        }

        ///<exception cref="SystemException"></exception>
        public async Task<List<string>> DeleteArticleTags(long inBlogArticleId)
        {
            return await Task.Run(
                () =>
                {
                    List<string> result = _blogDBContext.
                                            BlogArticlesTags.
                                            RemoveRange(_blogDBContext.
                                                            BlogArticlesTags.
                                                            Where(record => record.BlogArticleId == inBlogArticleId)).
                                            Select(record => record.TagValue).
                                            ToList();

                    _blogDBContext.SaveChanges();

                    return result;
                });
        }

        ///<exception cref="SystemException"></exception>
        public async Task<List<string>> DeleteAllTags()
        {
            return await Task.Run(
               () =>
               {
                   List<string> result = _blogDBContext.
                                           BlogArticlesTags.
                                           RemoveRange(_blogDBContext.
                                                           BlogArticlesTags).
                                           Select(record => record.TagValue).
                                           ToList();

                   _blogDBContext.SaveChanges();

                   return result;
               });
        }

        ///<exception cref=ArgumentNullException"></exception>
        public async Task<List<string>> GetArticleTags(long inBlogArticleId)
        {
            return await Task.Run(
                () =>
                    _blogDBContext.
                        BlogArticlesTags.
                        Where(record => record.BlogArticleId == inBlogArticleId).
                        Select(record => record.TagValue).
                        ToList());
        }

        ///<exception cref=ArgumentNullException"></exception>
        public async Task<List<string>> GetAllTags()
        {
            return await Task.Run(
                () =>
                _blogDBContext.
                    BlogArticlesTags.
                    Select(record => record.TagValue).
                    ToList());
        }

        //***       Comments

        ///<exception cref="SystemException"></exception>
        public async Task AddComment(BlogArticleComment inComment)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.
                        BlogArticleComments.
                        Add(inComment);

                    _blogDBContext.SaveChanges();
                });
        }

        ///<exception cref="SystemException"></exception>
        public async Task<BlogArticleComment> DeleteComment(long inCommentId)
        {
            return await Task.Run(
                () =>
                {
                    BlogArticleComment result = _blogDBContext.
                                                    BlogArticleComments.
                                                    Remove( _blogDBContext.
                                                                BlogArticleComments.
                                                                Attach(new BlogArticleComment
                                                                {
                                                                    Id = inCommentId
                                                                }));

                    _blogDBContext.SaveChanges();

                    return result;
                });
        }

        ///<exception cref="SystemException"></exception>
        public async Task UpdateComment(BlogArticleComment inComment)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.
                    Entry(_blogDBContext.
                                BlogArticleComments.
                                SingleOrDefault(record => record.Id == inComment.Id)).
                    CurrentValues.SetValues(inComment);

                    _blogDBContext.SaveChanges();
                });
        }
    }
}