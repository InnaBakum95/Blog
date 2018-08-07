using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBlogAPI.Components.BlogArticleComponents.Services.Interfaces
{
    public interface IBlogArticleService
    {
        ///<exception cref="System.SystemException"></exception>
#if FILES_TO_FILESYSTEM
        Task<BlogArticle> CreateArticle(BlogArticle blogArticle, string inBasePath);
#else
        Task<BlogArticle> CreateArticle(BlogArticle blogArticle);
#endif

        ///<exception cref="System.ArgumentNullException"></exception>
        Task<List<BlogArticle>> GetAllArticles();

        ///<exception cref="SystemException"></exception>
        Task<BlogArticle> GetArticle(long inArticleId);

        ///<exception cref="System.SystemException"></exception>
#if FILES_TO_FILESYSTEM
        Task<BlogArticle> DeleteArticle(long inArticleId, string inBasePath);
#else
        Task<BlogArticle> DeleteArticle(long inArticleId);
#endif

        ///<exception cref="System.SystemException"></exception>
        Task UpdateArticle(BlogArticle inArticle);

        //***       Tags

        ///<exception cref="SystemException"></exception>
        Task AddTag(BlogArticleTag inTag);

        ///<exception cref="System.SystemException"></exception>
        Task DeleteTag(BlogArticleTag inTag);

        ///<exception cref="System.SystemException"></exception>
        Task<List<string>> DeleteArticleTags(long inBlogArticleId);

        ///<exception cref="System.SystemException"></exception>
        Task<List<string>> DeleteAllTags();

        ///<exception cref="System.ArgumentNullException"></exception>
        Task<List<string>> GetArticleTags(long inBlogArticleId);

        ///<exception cref="System.ArgumentNullException"></exception>
        Task<List<string>> GetAllTags();

        //***       Comments

        ///<exception cref="System.SystemException"></exception>
        Task AddComment(BlogArticleComment inComment);

        ///<exception cref="System.SystemException"></exception>
        Task<BlogArticleComment> DeleteComment(long inCommentId);

        ///<exception cref="System.SystemException"></exception>
        Task UpdateComment(BlogArticleComment inComment);
    }
}
